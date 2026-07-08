# AbitElit - Admissions Manager 🎓

A full-stack web application for managing student admissions. Built with a clean N-Tier architecture, separating the API, business logic, and data access layers.

### 🛠️ Tech Stack & Architecture
* **Frontend (SPA):** Angular 22, TypeScript, Bootstrap 5.
* **Backend (API):** .NET 10, C#, ASP.NET Core Web API.
* **Database & ORM:** PostgreSQL, Entity Framework Core 10.
* **Security:** JWT Authentication (Role-Based Access: `Admin` / `Reader`).
* **Libraries:** `DocX` (Word document export), `Scalar.AspNetCore` (OpenAPI).

### 📁 Project Structure
* `AbitElit.WebApi/` – Presentation layer; contains controllers, routing, JWT configurations, and API endpoints.
* `AbitElit.BusinessLogic/` – Core logic; handles services (`ApplicantService`, `AuthService`), business rules, and Word generation.
* `AbitElit.DataAccess/` – Data layer; manages PostgreSQL connection, EF Core DbContext, migrations, and repositories.
* `AbitElit.Frontend/` – Client application; built with Angular, implementing standalone components, routing, and role-based views.

### ✨ Key Features
* **Role Management:** Admin (full CRUD) and Reader (view-only) access.
* **Applicant Data:** Add, edit, delete, and view applicant records.
* **Smart Filtering:** Filter candidates by minimum exam score and school number.
* **Reports:** Export filtered applicant lists to `.docx` format.

---

# AbitElit - Система обліку абітурієнтів факультету ЕЛIТ🎓

Full-stack веб-застосунок для управління даними абітурієнтів. Побудований за N-Tier архітектурою з чітким розділенням API, бізнес-логіки та роботи з даними.

### 🛠️ Стек технологій та Архітектура
* **Frontend (SPA):** Angular 22, TypeScript, Bootstrap 5.
* **Backend (API):** .NET 10, C#, ASP.NET Core Web API.
* **База даних та ORM:** PostgreSQL, Entity Framework Core 10.
* **Безпека:** JWT Авторизація (Ролі: `Admin` / `Reader`).
* **Бібліотеки:** `DocX` (експорт у Word), `Scalar.AspNetCore` (OpenAPI).

### 📁 Структура проєкту
* `AbitElit.WebApi/` – Рівень представлення; містить контролери, маршрутизацію, налаштування JWT та API ендпоінти.
* `AbitElit.BusinessLogic/` – Бізнес-логіка; сервіси (`ApplicantService`, `AuthService`), правила обробки даних та генерація Word-звітів.
* `AbitElit.DataAccess/` – Рівень роботи з даними; контекст EF Core, міграції, підключення до PostgreSQL та репозиторії.
* `AbitElit.Frontend/` – Клієнтська частина; застосунок на Angular (standalone-компоненти, роутинг та інтерфейси під різні ролі).

### ✨ Основний функціонал
* **Рольовий доступ:** Admin (всі CRUD-операції) та Reader (лише перегляд).
* **Дані абітурієнтів:** Створення, редагування, видалення та перегляд записів.
* **Фільтрація:** Пошук за мінімальним балом за іспит та номером школи.
* **Звіти:** Експорт відфільтрованих списків у формат `.docx`.
