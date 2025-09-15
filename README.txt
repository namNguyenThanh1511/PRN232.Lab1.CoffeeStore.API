# CoffeeStore.API - Docker Setup

Project này sử dụng **.NET 8 Web API** và **SQL Server 2022** chạy bằng Docker Compose.  
Bao gồm hướng dẫn chạy migration, kết nối cơ sở dữ liệu, và quản lý container.

---

## 🚀 Yêu cầu

- [Docker Desktop](https://www.docker.com/products/docker-desktop)  
- [.NET SDK 8.x](https://dotnet.microsoft.com/en-us/download)  

---

## 🛠 Cấu trúc project
PRN232.Lab1.CoffeeStore.API/
│── PRN232.Lab1.CoffeeStore.API
│── PRN232.Lab1.CoffeeStore.Data
       │── Configurations
       │── Entities
       │── Migrations
       │── Repositories
│── Dockerfile # Dockerfile để build API
│── docker-compose.yml # Docker Compose config
│── README.md # Hướng dẫn này


---

## 📦 Cách chạy bằng Docker

### 1. Build & chạy containers
```bash
docker-compose up -d --build





