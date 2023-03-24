dotnet clean

dotnet restore

dotnet build

dotnet run

dotnet tool install --global dotnet-ef --version 3.*

dotnet ef migrations add InitialCreate

dotnet ef database update

dotnet tool install --global dotnet-ef

dotnet watch run --launch-profile https

 "DefaultConnection": "server=192.185.11.184;userid=fernf_db;password=Mcs@12345;database=fernfers_flower_db;"
 "DefaultConnection": "server=localhost;userid=root;password=;database=fernfers_vaccinebe1;port=3306;"