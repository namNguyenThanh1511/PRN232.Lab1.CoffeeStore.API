import pandas as pd

# Đọc file Excel
df = pd.read_excel("login_testcases.xlsx")

# Xuất ra file JSON
df.to_json("login_testcases.json", orient="records", force_ascii=False, indent=4)

print("✅ File login_testcases.json created.")
