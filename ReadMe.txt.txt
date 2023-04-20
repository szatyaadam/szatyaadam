CookBook project;
//This project made by teamWork what is contains two people;
//The project Contains :
	Object contains =new Object
	{
	     "Vue 3 js frontend responsive webPage",
	     "a WPF console application that is available for the admins",
	     "Documentation for users and developers",
	     "MySQL Database",
	     "Rest API project what is communicate the others others and provides the data"
	};
//For testing or view the project you need to have :
				List<string> YouNeedToHave=new List<string>
					{
					"PhpMyAdmin(start the MySQL server, and add a new Database into it"+
					   "you can find the SQL file here: CookBookProject/Adatbázis/cookbook04.14.sql, who is provides the Existed data)",
					 "Visual Studio",
					 "Visual Studio Code"
					};

//We divided the tasks but also work together in the same project part;
//The Database and the WPF Desktop Application is my own parts;
//The whole project essence is to make a Program where the User can:

						Register(UserName,Password,Email);
						//With Token authentication;
						Login(UserName,Passord);
						
						if (Login())
							{
								FindAllRecepts();
								CreateNewRecept();
								ModifyOwnRecept();
								DeleteOwnRecept();
								AddToFavorits();
							}
						else FindAllRecepts();

//You can find more details and description in :\Documentation\Developer_User_documentation;

	
	
