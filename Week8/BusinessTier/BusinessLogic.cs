namespace Week8;
using System.Data;
using MySql.Data.MySqlClient;
class BusinessLogic
{
   
    static void Main(string[] args)
    {
        bool _continue = true;
        User user;
        GuiTier appGUI = new GuiTier();
        DataTier database = new DataTier();

        // start GUI
        user = appGUI.Login();

       
        if (database.LoginCheck(user)){

            while(_continue){
                int option  = appGUI.Dashboard(user);
                switch(option)
                {
                    // check enrollment
                    case 1:
                        Console.WriteLine("Please input a semester");
                        string semester = Console.ReadLine();
                        DataTable tableEnrollment = database.CheckEnrollment(user, semester);
                        if(tableEnrollment != null)
                            appGUI.DisplayEnrollment(tableEnrollment);
                        break;
                    // Add A Course
                    case 2:
                        DataTable tableAddEnrollment = database.ShowAllCourse();
                        if(tableAddEnrollment != null)
                            appGUI.DisplayCourse(tableAddEnrollment);
                        Console.WriteLine("Please input a CourseID");
                        int courseID = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("Please input a semester in TermYear format, e.g: Fall2022, Spring2021");
                        semester = Console.ReadLine();
                        database.AddCourse(user, courseID,semester);
                        tableAddEnrollment = database.CheckEnrollment(user,semester);
                        if(tableAddEnrollment != null)
                            appGUI.DisplayCourse(tableAddEnrollment);
                        break;
                    // Drop A Course
                    case 3:
                        Console.WriteLine("Please input a semester in TermYear format, e.g: Fall2022, Spring2021");
                        semester = Console.ReadLine();
                        DataTable tableEnrollmentD = database.CheckEnrollment(user, semester);
                        if(tableEnrollmentD != null)
                            appGUI.DisplayEnrollment(tableEnrollmentD);
                        
                        Console.WriteLine("Please input a CourseID");
                        courseID = Convert.ToInt16(Console.ReadLine());
                        database.DropCourse(user, courseID, semester);
                        tableEnrollmentD = database.CheckEnrollment(user, semester);
                        if(tableEnrollmentD != null)
                            appGUI.DisplayEnrollment(tableEnrollmentD);

                        break;
                    // Log Out
                    case 4:
                        _continue = false;
                        Console.WriteLine("Log out, Goodbye.");
                        break;
                    // default: wrong input
                    default:
                        Console.WriteLine("Wrong Input");
                        break;
                }

            }
        }
        else{
                Console.WriteLine("Login Failed, Goodbye.");
        }        
    }    
}
