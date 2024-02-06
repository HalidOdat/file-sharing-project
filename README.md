# Shop

### **The Onion Architecture consists of four main layers:**

Necessary packages to install in the layers:
- **Domain Layer**
    - Microsoft.AspNetCore.Identity.UI - 8.0.1
- **Infrastructure Layer (Repository or Data)**
    - Microsoft.AspNetCore.Identity.EntityFrameworkCore - 8.0.1
    - Microsoft.EntityFrameworkCore.Design - 8.0.1
- **Application Services Layer (Service or Application)**
- **Presentation Layer (Web)**
    - Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore - 8.0.1
    - Microsoft.AspNetCore.Identity.EntityFrameworkCore - 8.0.1
    - Microsoft.AspNetCore.Identity.UI - 8.0.1
    - Microsoft.EntityFrameworkCore.Tools - 8.0.1
    - Npgsql.EntityFrameworkCore.PostgreSQL - 8.0.0

Commands in order to interact with the migrations:
- `dotnet ef migrations list` - listing all migrations
- `dotnet ef migrations add [name of the new migration]` - adding new migration
- `dotnet ef migrations remove` - removing the last migration
- `dotnet ef database update` - updating the database after adding new migration
