from openpyxl import Workbook
import json
from datetime import datetime, timedelta

# === Setup workbook ===
wb = Workbook()
ws = wb.active
ws.title = "Login Test Cases"

# === Header columns ===
headers = [
    "TestCaseID", "TestName", "Description", "Method",
    "Endpoint", "RequestBody", "ExpectedStatus",
    "ExpectedResponse", "Notes"
]
ws.append(headers)

endpoint = "/api/auth/login"


# === Unified response structure ===
def make_response(is_success=True, message="string", data=None, errors=None):
    """
    Tạo cấu trúc response. 
    - Nếu is_success = True => có 'data'
    - Nếu is_success = False => không có key 'data'
    """
    base = {
        "isSuccess": is_success,
        "message": message,
        "errors": errors or []
    }

    if is_success:
        base["data"] = data or {
            "accessToken": "string",
            "refreshToken": "string",
            "accessTokenExpiry": (datetime.utcnow() + timedelta(hours=1)).isoformat() + "Z",
            "refreshTokenExpiry": (datetime.utcnow() + timedelta(days=7)).isoformat() + "Z",
            "tokenType": "Bearer"
        }

    return base


# === Test cases ===
test_cases = [
    {
        "id": "TC01",
        "name": "Login with valid email",
        "desc": "User logs in successfully using email",
        "method": "POST",
        "body": {"keyLogin": "nam@example.com", "password": "123456"},
        "status": 200,
        "response": make_response(True, "Login successful"),
        "notes": "Verify token structure"
    },
    {
        "id": "TC02",
        "name": "Login with valid phone number",
        "desc": "User logs in successfully using phone number",
        "method": "POST",
        "body": {"keyLogin": "0937634399", "password": "123456"},
        "status": 200,
        "response": make_response(True, "Login successful"),
        "notes": "Verify both tokens exist"
    },
    {
        "id": "TC03",
        "name": "Missing keyLogin",
        "desc": "Return 400 when keyLogin is missing",
        "method": "POST",
        "body": {"password": "ValidPass123"},
        "status": 400,
        "response": make_response(
            False,
            "Dữ liệu không hợp lệ",
            errors=[{"message": "Email hoặc số điện thoại không được để trống"}]
        ),
        "notes": "Required field validation"
    },
    {
        "id": "TC04",
        "name": "Missing password",
        "desc": "Return 400 when password is missing",
        "method": "POST",
        "body": {"keyLogin": "user@example.com"},
        "status": 400,
        "response": make_response(
            False,
            "Dữ liệu không hợp lệ",
            errors=[{"message": "Mật khẩu không được để trống"}, {"message": "Mật khẩu phải từ 6-100 ký tự"}]
        ),
        "notes": "Required field validation"
    },
    {
        "id": "TC05",
        "name": "Invalid email format",
        "desc": "Return 400 for invalid email format",
        "method": "POST",
        "body": {"keyLogin": "invalid_email", "password": "ValidPass123"},
        "status": 400,
        "response": make_response(
            False,
            "Vui lòng nhập đúng định dạng email hoặc số điện thoại"
        ),
        "notes": "Regex validation"
    },
    {
        "id": "TC06",
        "name": "Non-existent user",
        "desc": "Return 404 when user not found",
        "method": "POST",
        "body": {"keyLogin": "unknown@example.com", "password": "SomePass123"},
        "status": 404,
        "response": make_response(
            False,
            "Tài khoản không tồn tại"
        ),
        "notes": "User lookup"
    },
    {
        "id": "TC07",
        "name": "Inactive user",
        "desc": "Return 401 when account is locked or disabled",
        "method": "POST",
        "body": {"keyLogin": "john@example.com", "password": "123456"},
        "status": 401,
        "response": make_response(
            False,
            "Tài khoản đã bị vô hiệu hóa"
        ),
        "notes": "Account status validation"
    },
    {
        "id": "TC08",
        "name": "Incorrect password",
        "desc": "Return 401 when password is incorrect",
        "method": "POST",
        "body": {"keyLogin": "user@example.com", "password": "WrongPass"},
        "status": 401,
        "response": make_response(
            False,
            "Không được phép truy cập",
            errors=[{"message": "Mật khẩu không chính xác"}]
        ),
        "notes": "Invalid credentials"
    },
    {
        "id": "TC09",
        "name": "Empty fields",
        "desc": "Return 400 when both fields are empty",
        "method": "POST",
        "body": {"keyLogin": "", "password": ""},
        "status": 400,
        "response": make_response(
            False,
            "Dữ liệu không hợp lệ",
            errors=[{"message": "Thông tin đăng nhập không được để trống"}]
        ),
        "notes": "Empty input validation"
    },
    {
        "id": "TC10",
        "name": "Overlength keyLogin",
        "desc": "Return 400 when keyLogin exceeds 255 chars",
        "method": "POST",
        "body": {"keyLogin": "a" * 256 + "@gmail.com", "password": "ValidPass123"},
        "status": 400,
        "response": make_response(
            False,
            "Dữ liệu không hợp lệ",
            errors=[{"message": "Độ dài không được vượt quá 255 ký tự"}]
        ),
        "notes": "Length validation"
    },
]

# === Write to Excel ===
for tc in test_cases:
    ws.append([
        tc["id"],
        tc["name"],
        tc["desc"],
        tc["method"],
        endpoint,
        json.dumps(tc["body"], ensure_ascii=False, indent=2),
        tc["status"],
        json.dumps(tc["response"], ensure_ascii=False, indent=2),
        tc["notes"]
    ])

# === Auto adjust column width ===
for col in ws.columns:
    max_len = 0
    col_letter = col[0].column_letter
    for cell in col:
        if cell.value:
            max_len = max(max_len, len(str(cell.value)))
    ws.column_dimensions[col_letter].width = max_len + 2

# === Save file ===
wb.save("login_testcases.xlsx")
print("✅ File 'login_testcases.xlsx' created successfully!")
