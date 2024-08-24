# Read Realm - Backend

## Introduction
This application is designed to enhance the reading experience by enabling users to track their progress, make notes, and share insights with friends—all while staying spoiler-free. This repository contains the backend logic of the application.

### Key Features:

- **Track Your Reading Progress**: The app allows for detailed recording of reading journeys, including start and finish dates, chapter numbers, and more.
- **Make and Share Notes**: Users can create notes during reading sessions, which can be kept private, shared with friends, or made public for all to see. Notes can vary in detail, catering to different preferences.
- **Add and Connect with Friends**: Users can create notes during reading sessions, which can be kept private, shared with friends, or made public for all to see. Notes can vary in detail, catering to different preferences.
- **View and Discuss Mutual Books**: The app highlights books that users and their friends are reading simultaneously. It also allows viewing each other’s notes on these books, ensuring a spoiler-free experience.

## Installation
### Prerequisites
Before you begin, ensure you have the following installed:

- .NET 8 SDK
- SQL Server Management Studio (SSMS) (optional, but useful for managing your database)
- Git (for cloning the repository)
-  A hosted MSSQL Server instance (local or remote)

### Step 1: Clone the Repository
Clone the project repository to your local machine using Git.
```
git clone https://github.com/anjaaivanovic/readRealm.git
cd readRealm
```

### Step 2: Configure the Database Connection
1. Open the project in your preferred IDE (e.g., Visual Studio or Visual Studio Code).
2. Locate the *appsettings.json* file in the root of the project.
3. Update the ConnectionStrings section with your MSSQL Server details:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=your-server-address;Database=your-database-name;User Id=your-username;Password=your-password;"
}
```

### Step 3: Reverse Engineer the Database
This project uses EF Core Power Tools to generate entity classes and the DbContext from an existing database. After cloning the repository, follow these steps to reverse engineer the database:
If you haven't installed EF Core Power Tools yet, you can do so via Visual Studio by going to `Extensions > Manage Extensions and searching for "EF Core Power Tools`."

**Configure Reverse Engineering**

1. In Visual Studio, right-click on the project *Models* in the Solution Explorer.
2. Choose `EF Core Power Tools > Reverse Engineer`.
3. In the dialog that appears, select your database connection (you can use the connection string provided in the *appsettings.json* or create a new one).
4. Choose the tables you want to include in your model.
5. Configure the output directories and naming conventions. Set the `EntityTypes path` to ***Entities***. Set the naming convention to ***Pluralize or singularize generated object names (English)***. Go to `Advanced > DbContext path` and set it to ***Context***.
6. Generate the Model

### Step 4: Run the Project
Run the project using the .NET CLI:

```
dotnet run

```
