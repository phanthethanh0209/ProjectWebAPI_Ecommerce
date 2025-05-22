# 🛒 ProjectWebAPI_Ecommerce

## 📜 Giới thiệu

`ProjectWebAPI_Ecommerce` là một hệ thống Web API cho ứng dụng thương mại điện tử, được được xây dựng bằng ASP.NET Core và phát triển theo kiến trúc Clean Architecture, sử dụng CQRS, Entity Framework Core, JWT và xử lý tác vụ nền bằng Hangfire. Dự án tích hợp các tính năng chính như xác thực, phân quyền theo Role và Permission, quản lý sản phẩm, xử lý đơn hàng, thanh toán Stripe.

## ⚙️ Công nghệ sử dụng

- ASP.NET Core 8.0
- Entity Framework Core (Code-First ,Fluent API)
- SQL Server (lưu trữ dữ liệu)
- Clean Architecture + CQRS + MediatR
- Xác thực JWT Authentication
- AutoMapper (Chuyển đổi dữ liệu giữa DTO và Model)
- Swagger UI (Tài liệu API)
- Repository Pattern (Quản lý dữ liệu)
- Validation (Kiểm tra dữ liệu đầu vào sử dụng Fluent Validation)
- Hangfire cho tác vụ nền
- Stripe API cho thanh toán

## 📂 Cấu trúc dự án

Dự án được tổ chức theo nguyên tắc Clean Architecture:
- **ECommerce.API**: Chứa các controller API, middleware, template email và cấu hình
- **ECommerce.Application**: Chứa logic nghiệp vụ, handler CQRS, DTO và service
- **ECommerce.Domain**: Định nghĩa các entity, enum
- **ECommerce.Infrastructure**: Triển khai repository, DbContext, JWT và tích hợp dịch vụ bên ngoài như Hangfire, Stripe

```
ECommerce/
├── WebAPI
│   ├── Controllers
│   └── Middleware
│   └── Templates
├── Application
│   ├── Common
│   ├── Features (CQRS: Commands / Queries / Handlers)
│   └── Interfaces
├── Domain
│   ├── Entities
│   └── Enums
├── Infrastructure
│   ├── Authentication
│   ├── BackgroundJobs
│   ├── Data
│   ├── Migrations
│   ├── Repositories
│   ├── Services
```

## 🚀 Tính năng

- Đăng ký và đăng nhập người dùng với xác thực JWT (JSON Web Tokens)
- Quản lý người dùng: Xác thực, phân quyền và quản lý thông tin người dùng
- Quản lý danh mục 
- Quản lý sản phẩm (API công khai để lấy danh sách sản phẩm hoặc sản phẩm theo ID)
- Tạo và quản lý đơn hàng (Phân trang, lọc, tìm kiếm đơn hàng theo trạng thái, theo thời gian)
- Xác thực: JWT Authentication (JSON Web Tokens), Refresh Token
- Phân quyền theo Role & Permission (Admin, Customer, Manager) với Custom Authorization
- Xử lý thanh toán qua Stripe (sử dụng Stripe Webhook để cập nhật trạng thái đơn hàng)
- Gửi email xác nhận đơn hàng và cập nhật trạng thái đơn hàng (sử dụng BackgroundJob Hangfire)

## 🛠️ Phân quyền

Hệ thống sử dụng bảng `Roles`, `Permissions`, `RolePermissions`, `UserRoles`.  
Tất cả được ánh xạ tự động và kiểm tra bằng Attribute `HasPermission("View.Order")`.  
Áp dụng `CustomAuthorizationHandler` để xử lý logic kiểm tra quyền dựa vào JWT + cơ sở dữ liệu.

## ⏳ BackgroundJob

Dự án sử dụng Hangfire để xử lý các tác vụ nền, bao gồm:
- Gửi email xác nhận đơn hàng sau khi thanh toán thành công
- Hủy đơn hàng nếu chưa thanh toán sau 15 phút
Có thể truy cập dashboard tại `/hangfire` để theo dõi job

## 🔄 Cổng thanh toán Stripe

Cách gọi và sử dụng thanh toán Stripe và webhook Stripe:
- Cấu hình StripeSettings trong appsettings.json
- Chạy Stripe CLI
- Gọi API tạo hóa đơn (api/Order/checkout)
- Gọi API tạo thanh toán (api/Payment/create-payment-intent) sử dụng orderId vừa tạo
- Dùng clientSecret paste vào code frontend (stripepayment.html) để test nhập trên giao diện
- Chạy web và nhập thông tin card -> Submit

Các bước sử dụng Stripe CLI
- Tải file stripe mới nhất từ link: https://github.com/stripe/stripe-cli/releases/tag/v1.26.1
- Cài đặt biến môi trường cho Stripe
- chạy stripe listen
  ```bash
  stripe listen --forward-to https://localhost:xxxxx/api/stripe/webhook
  ```
## 🛠️ Cài đặt

1. Clone repository:
   ```bash
   git clone https://github.com/phanthethanh0209/ProjectWebAPI_Ecommerce.git
   ```
2. Cấu hình database trong `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Data Source=YOUR_SQLSERVER;Initial Catalog=ECommerceDB;User=YOUR_USER;Password=YOUR_PASSWORD;"
   }
   ```

3. Mở **Package Manager Console** (Tools > NuGet Package Manager > Package Manager Console) và chạy lệnh sau để tạo migration:
```powershell
Add-Migration InitialCreate
```
4. Sau đó, cập nhật database:
```powershell
Update-Database
```

## 📚 API Documentation

Sử dụng **Swagger** để xem tài liệu API
Hoặc sử dụng **Postman** để kiểm thử API

## 🤝 Đóng góp

Nếu bạn muốn đóng góp, hãy tạo **Pull Request** hoặc báo lỗi trong mục **Issues** của GitHub.

## 📩 Liên hệ
Để biết thêm chi tiết hoặc gửi phản hồi, liên hệ qua [phanthethanh0209@gmail.com].
