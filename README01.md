# WanderDragon

dotnet migrations:
-more than one db context:
  dotnet ef migrations add (migration name) --context (new context file name without the .cs) -o target file
  dotnet ef database update (migration name) --context (new context file name without the .cs) (-o target file)optional
  
 for more migration commands go to:
 https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet
 
