
# 📌 API - Back-End LevelUp (.NET Core 8)

This API provides remote API (https://www.freetogame.com/api-doc) search functionality and stores the search in local database

---

## 🔧 Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [SQLite](https://sqlite.org/)  
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [xUnit](https://xunit.net/index.html) 

---

## 🚀 Run localhost API

```bash
# 1. Clone the repository
git clone https://github.com/jonasroliveira/BackEnd_LevelUp.git
cd BackEnd_LevelUp

# 2. Restore dependencies
dotnet restore

# 3. Start API
dotnet run

- Database automatically create in the application folder after run

# 4. Address swagger
http://localhost:5052/swagger/index.html

# 5. Endpoints
http://localhost:5052/api/recommend
http://localhost:5052/api/recommendations

# Post RecommendController
http://localhost:5052/api/recommend
{
  "genres": [
    "string"
  ],
  "platform": "string", //pc, browser or both
  "minRamGb": 0 //Must be greater than zero
}

# Get RecommendationsController
http://localhost:5052/api/recommendations
No parameters

# Run tests
In folder BackEnd_LevelUp
Command: dotnet test