/*
 * CLASS        : Run
 * 
 * DESCRIPTION  : This class contains the heavy logic for the program
 *      it has the methods required to add, delete and display a list of the club's members.
 *      It can also save that list to a file, or load a list of members from a file.
 *      
 *      At this time, this class also holds the filehandling and parsing logic required
 *      Ideally, I would move this to a separate class, but it would take some time to separate 
 *      them safely
 * 
 */
namespace A03_OOP_JoshuaYDB
{
    internal class Run
    {
        public int RunProgram()
        {

            Boolean ready = true; //flag for the menu loop, will terminate the loop when False
            ConsoleKeyInfo user_input; //using the ConsoleKeyInfo object/struct to store the key stroke for menu input


            //create a collection to store the list of members
            List<Member> members = new List<Member>();


            //loop for main program/menu
            while (ready)
            {
                UserInterface.Display_menu(); //menu display

                user_input = Console.ReadKey();
                switch (user_input.Key)
                {
                    //add a member
                    case ConsoleKey.A:
                        
                        members.Add(new Member());
                        //get the size of the list to find the index of the last member in the list
                        int ix_last_member = (members.Count)-1;

                        members[ix_last_member].Member_id = Create_ID();
                        members[ix_last_member].Member_first_name = Get_First();
                        members[ix_last_member].Member_last_name = Get_Last();
                        members[ix_last_member].Member_email = Get_Email();
                        members[ix_last_member].Member_dob = Get_DOB();

                        UserInterface.Clear_screen();
                        UserInterface.Display_message($"{members[ix_last_member].Member_first_name} {members[ix_last_member].Member_last_name} was successfully added to the club!");
                        UserInterface.Block_program("Be sure to welcome them!\nPress any key to return to the menu...");

                        break;

                    //display all members
                    case ConsoleKey.L:
                        //method to display all members will run a loop through the list and
                        //call Display_one_member() each time iteration
                        //UserInterface.Display_one_member();
                        UserInterface.Clear_screen();
                        if(members.Count > 0)
                        {
                            foreach (Member member in members)
                            {
                                UserInterface.Display_one_member
                                    (member.Member_id.ToString("N"),
                                    member.Member_first_name,
                                    member.Member_last_name,
                                    member.Member_email,
                                    member.Member_dob.ToString(),
                                    (Get_Age(member)).ToString());
                                    //could add a delay here to make it print out line by line like a movie
                            }
                            UserInterface.Block_program();
                        }
                        else
                        {
                            UserInterface.Display_error_message
                                (
                                    "There are no members to display.",
                                    "Press any key to return to the menu..."
                                );
                        }
                        break;

                    //remove a member
                    case ConsoleKey.D:
                        UserInterface.Clear_screen();
                        //if there are any strings in the list to remove
                        if (members.Count > 0)
                        {
                            if (Remove_Member(members))
                            {
                                UserInterface.Clear_screen();
                                UserInterface.Display_message("Deletion successful.");
                                UserInterface.Block_program();
                            }
                            else
                            {
                                //deletion not successful, break and reloop
                            }
                        }
                        //if there are no strings in the list to remove
                        else
                        {
                            UserInterface.Display_error_message
                                (
                                    "There are no members to remove. Please add a member before removing one.",
                                    "Press any key to return to the menu..."
                                );
                        }

                        break;

                    case ConsoleKey.S:
                        //method to save members to file
                        Get_valid_file_name(members);
                        break;

                    case ConsoleKey.O:
                        //method to load members from file
                        Find_Loadable_File(members);
                        break;

                    case ConsoleKey.X:

                        UserInterface.Clear_screen();
                        UserInterface.Display_message("Are you sure you want to exit (Y/N)?");
                        user_input = Console.ReadKey();

                        if (user_input.Key == ConsoleKey.Y)
                        {
                            ready = false; //change the flag and exit the loop
                            UserInterface.Clear_screen();
                        }
                        // i.e. if input is not Y or y
                        else
                        {
                            //do nothing to continue looping to the menu
                        }
                        break;

                    default:
                        //no menu option selected, display an error
                        UserInterface.Display_error_message
                            (
                                "Invalid selection. Choose an operation based on the characters in brackets <>.",
                                "Press any key to return to the menu..."
                            );
                        break;

                }

            }

            return 0;
        }


        //____Methods that gather data/input for a new member____

        /*
         * METHOD       : Create_ID
         * 
         * DESCRIPTION  : Creates a unique ID of many hex digits statistically unlikely to be duplicated
         * 
         * PARAMETERS   : NONE
         * 
         * RETURNS      : Guid unique_id: a unique id
         */
        internal Guid Create_ID()
        {
            Guid unique_id = Guid.NewGuid();
            return unique_id;
        }


        /*
         * METHOD       : Get_First
         * 
         * DESCRIPTION  : gets input from the user for a first name of a new member
         * 
         * PARAMETERS   : NONE
         * 
         * RETURNS      : string first_name : a first name
         */
        internal string Get_First()
        {
            Boolean invalid_name = true;
            string first_name = "x";
            string? input = null;
            
            while(invalid_name)
            {
                UserInterface.Clear_screen();
                UserInterface.Display_message
                    (
                        "**Adding new member (Step 1/4)**\n" +
                        "Enter the first name of the new member:\n"
                    );
                input = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(input))
                {
                    UserInterface.Display_error_message
                        (
                            "The first name may not be blank",
                            "Press any key to try again..."
                        );
                }
                else if(!String.IsNullOrWhiteSpace(input))
                {
                    first_name = input;
                    invalid_name = false;
                }
            }
            return first_name;
            
        }


        /*
         * METHOD       : Get_Last
         * 
         * DESCRIPTION  : gets input from the user for a Last name of a new member
         * 
         * PARAMETERS   : NONE
         * 
         * RETURNS      : string last_name: a last name
         */
        internal string Get_Last()
        {
            Boolean invalid_name = true;
            string last_name = "x";
            string? input = null;

            while (invalid_name)
            {
                UserInterface.Clear_screen();
                UserInterface.Display_message
                    (
                        "**Adding new member (Step 2/4)**\n" +
                        "Enter the last name of the new member:\n"
                    );
                input = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(input))
                {
                    UserInterface.Display_error_message
                        (
                            "The last name may not be blank",
                            "Press any key to try again..."
                        );
                }
                else if(!String.IsNullOrWhiteSpace(input))
                {
                    last_name = input;
                    invalid_name = false;
                }
            }
            return last_name;

        }


        /*
         * METHOD       : Get_Email
         * 
         * DESCRIPTION  : gets input from the user for a an email of a new member
         * 
         * PARAMETERS   : NONE
         * 
         * RETURNS      : string email: an email address
         */
        internal string Get_Email()
        {
            Boolean invalid_email = true;
            string? input;
            string email = "x";

            while (invalid_email)
            {
                UserInterface.Clear_screen();
                UserInterface.Display_message
                    (
                        "**Adding new member (Step 3/4)**\n" +
                        "Enter the email address of the new member:\n"
                    );
                input = Console.ReadLine();
                //ideally, this would have its own email address validation, and more complex systems would require validation with a link
                if (String.IsNullOrWhiteSpace(input))
                {
                    UserInterface.Display_error_message
                        (
                            "The email may not be blank",
                            "Press any key to try again..."
                        );
                }
                else if (!String.IsNullOrWhiteSpace(input))
                {
                    email = input;
                    invalid_email = false;
                }
            }
            return email;

        }



        /*
         * METHOD       : Get_DOB
         * 
         * DESCRIPTION  : gets input from the user for the date of birth of a new member
         * 
         * PARAMETERS   : NONE
         * 
         * RETURNS      : DateOnly dob: a date of birth
         */
        //get the DOB of the new member
        internal DateOnly Get_DOB()
        {
            Boolean invalid_dob = true;
            string? input;
            DateOnly dob = new DateOnly(1900, 01, 01);

            while (invalid_dob)
            {
                UserInterface.Clear_screen();
                UserInterface.Display_message
                    (
                        "**Adding new member (Step 4/4)**\n" +
                        "Enter the Date of birth of the new member YYYY-MM-DD:\n"
                    );
                input = Console.ReadLine();
                //if the date is not parsable
                if (!DateOnly.TryParse(input, out dob))
                {
                    UserInterface.Display_error_message
                        (
                            "The date of birth entered was invalid.\nIt may not be blank and should be formated as YYYY-MM-DD.",
                            "Press any key to try again..."
                        );
                }
                //if parse successful
                else if (DateOnly.TryParse(input, out dob))
                {
                    DateOnly.TryParse(input, out dob);
                    invalid_dob = false;
                }
            }
            return dob;
        }


        /*
         * METHOD       : Get_Age
         * 
         * DESCRIPTION  : Helper method that calculates a member's age based on their DOB and returns it as an int
         * 
         * PARAMETERS   : Member member: an object "Member" from the Member class, which is a person in the club
         * 
         * RETURNS      : int age: the member's age
         */
        // sources used
        //https://learn.microsoft.com/en-us/dotnet/api/system.datetime.now?view=net-10.0 DateTime and DateTime.Now structs
        //https://learn.microsoft.com/en-us/dotnet/standard/datetime/how-to-use-dateonly-timeonly DateOnly structs
        //https://learn.microsoft.com/en-us/dotnet/standard/datetime/how-to-use-dateonly-timeonly#add-or-subtract-days-months-years add or subtract dateOnly structs
        internal int Get_Age(Member member)
        {
            //creates new dateTime struct
            DateTime today = new DateTime();
            //gets the current date and time
            today = DateTime.Now;

            //converts the birthday to a dateTime, assumed at midnight (MinValue)
            DateTime mem_bday = member.Member_dob.ToDateTime(TimeOnly.MinValue);

            //access the years and subtract them
            int age;
            age = today.Year - mem_bday.Year;


            //if the current date is before the birthmonth
            if (today.Month < mem_bday.Month)
            {
                //they haven't had they're bday yet
                age -= 1;

            }
            //if the current date is the birthmonth
            else if (today.Month < mem_bday.Month)
            {
                //if the current date is before the day of the birthdate
                if (today.Day < mem_bday.Day)
                {
                    //they haven't had they're bday yet
                    age -= 1;
                }

            }
            return age;
        }




        //____File handling methods below____
        //ideally, these could be moved to separate class



        /*
         * METHOD       : Get_valid_file_name
         * 
         * DESCRIPTION  : Asks the user to input a file name (or path and file name) to which the list of members should be saved (parsable)
         * 
         * PARAMETERS   : List<Member> list_of_members: the list of objects that holds all the members
         * 
         * RETURNS      : NOTHING
         */
        internal void Get_valid_file_name(List<Member> list_of_members)
        {
            ConsoleKeyInfo user_input;
            Boolean invalid_file_name = true; //flag for a while loop
            string? file_name; //? allows null in order for validation to be done by the code instead of an exception or warning being thrown

            while (invalid_file_name)
            {
                UserInterface.Clear_screen();

                UserInterface.Display_message
                    (
                        "Enter the name of the file to which you would like to save the list of members." +
                        "\nNB:Avoid use of invalid characters. Be sure to specify file extensions as needed:\n"
                    );
                file_name = Console.ReadLine();

                //Preventing null or blank filenames. Regex might be preferable here if there were more specific requirements or file naming conventions to follow.
                //Exceptions around filehandling will be handled in a different method
                if (string.IsNullOrWhiteSpace(file_name))
                {
                    UserInterface.Display_error_message
                        (
                            "The file name may not be blank.",
                            "Press any key to enter a different file name."
                        );
                }
                //else, i.e. if filename is not blank or null, call the method to open and write to the file
                else
                {
                    //add .txt to the file extension if the extension is not .txt
                    if (file_name.Contains("."))
                    {
                        //find the index of the las period
                        int ix_last_period = file_name.LastIndexOf(".");
                        //replace the extension with .txt, but only the last extension (note.tar.gz becomes note.tar.txt)
                        file_name = file_name.Substring(0, ix_last_period) + ".txt";

                    }
                    //if no existing extension, add .txt
                    else
                    {
                        file_name = file_name + ".txt";
                    }

                    //confirm overwrite if file already exists
                    if (File.Exists(file_name))
                    {
                        UserInterface.Display_message
                            ($"A file named \"{file_name}\" already exists.\n\n" +
                            $"***ARE YOU SURE YOU WOULD LIKE TO OVERWRITE IT?***\n" +
                            $"<Y>: Confirm and overwrite\n" +
                            $"<N>: Cancel and return to name selection\n");

                        user_input = Console.ReadKey();

                        if (user_input.Key == ConsoleKey.Y)
                        {
                            //stay on track
                            UserInterface.Clear_screen();
                        }
                        // i.e. if input is not Y or y
                        else
                        {
                            //Re-loop the while
                            continue;
                        }
                    }
                    //if file doesn't already exist
                    else
                    {
                        //do nothing, stay on track
                    }


                    //if the file saving is successful
                    if (Save_Members_List(file_name, list_of_members))
                    {
                        //exit the loop by changing the flag
                        invalid_file_name = false;

                        UserInterface.Display_message($"\nData saved successfully to {file_name}.");
                        UserInterface.Block_program("Press any key to continue...");
                    }
                    //else, i.e. if the file saving is unsuccessful
                    else
                    {
                        //if the saving is unsuccessful, error handling and messages will be displayed by Save_Members_List()
                        //return to the main menu
                        invalid_file_name = false; //exit the loop
                        break;
                    }

                }

            }
            return;
        }


        /*
         * METHOD       : Save_Members_List
         * 
         * DESCRIPTION  : opens the file streams and tries to create or overwrite and then write the list to the specified file
         *              : Handles generic exceptions in case of failure.
         * 
         * PARAMETERS   : string file_name: The name of the file that the list of members will be saved to
         *              : List<Member> list_of_members: [shadowed param name] the list of objects that holds all the members
         * 
         * RETURNS      : Boolean success_flag: a true/false flag to indicate if the operation was successful
         */
        internal Boolean Save_Members_List(string file_name, List<Member> list_of_members)
        {
            Boolean success_flag;

            FileStream my_file_stream;
            StreamWriter my_stream_writer;

            //try to open and write
            try
            {
                //open streams
                my_file_stream = File.OpenWrite(file_name);
                my_stream_writer = new StreamWriter(my_file_stream);

                //write each element of the list to the file, line by line
                foreach (Member member in list_of_members)
                {
                    my_stream_writer.WriteLine($"{member.Member_id.ToString("N")}|{member.Member_first_name}|{member.Member_last_name}|{member.Member_email}|{member.Member_dob.ToString()}");
                }

                //close streams when finished
                my_stream_writer.Close();
                my_file_stream.Close();
                success_flag = true; //flag is true to indicate a successful save
            }
            //catch all exceptions
            catch (Exception ex)
            {
                //ex itself passes a very detailed error message. Opting for ex.Message, which passes a much shorter error message.
                UserInterface.Display_error_message
                (
                    "An error has occurred while saving the data to the file. The data was not saved.\n" + "Error information: \"" + ex.Message + "\"",
                    "Press any key to return to the menu..."
                );
                success_flag = false; //flag is false to indicate an error occurred
            }
            finally
            {
                UserInterface.Clear_screen();
            }

            return success_flag;

        }


        /*
         * METHOD       : Remove_Member
         * 
         * DESCRIPTION  : Method to remove a member from the specified list
         * 
         * PARAMETERS   : List<Member> list_of_members: the list of objects that holds each of the members
         * 
         * RETURNS      : Boolean success_flag: a true/false flag to indicate if the operation was successful
         */
        internal Boolean Remove_Member(List<Member> list_of_members)
        {
            Boolean success_flag = false;
            Boolean loop_control_flag = true;

            while (loop_control_flag)
            {
                string? user_input;
                ConsoleKeyInfo confirmation_input;

                UserInterface.Clear_screen();
                UserInterface.Display_message("Enter the Member ID of the Member you would like to delete.\n");
                user_input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(user_input))
                {
                    UserInterface.Clear_screen();
                    UserInterface.Display_message("The Member ID must not be blank or contain any spaces.");
                    UserInterface.Display_message("Press <Y> to type a new Member ID or\nPress <X> to return to the main menu.");
                    confirmation_input =  Console.ReadKey();
                    switch (confirmation_input.Key)
                    {
                        case ConsoleKey.Y:
                            //continue the loop, do nothing
                            break;
                        default:
                            loop_control_flag = false;
                            break;
                    }
                }
                //if the entry is not null, check if a member with that ID exists
                else
                {
                    //https://stackoverflow.com/questions/17264281/get-the-index-of-item-in-a-list-given-its-property showed me how to match a property value and find the index of that property's object within a collection
                    //checks each member's member ID [member.Member_id] as a string (without dashes "N") against the user_input. if none match the input, returns -1
                    int ix_for_removal = list_of_members.FindIndex(member => member.Member_id.ToString("N") == user_input);
                    if (ix_for_removal != -1)
                    {
                        UserInterface.Clear_screen();
                        UserInterface.Display_message
                            (
                                $"***ARE YOU SURE YOU WOULD LIKE TO DELETE THIS MEMBER?***\n\n"
                            );
                        UserInterface.Display_one_member(list_of_members[ix_for_removal]);
                        UserInterface.Display_message
                            (
                                $"\n<Y>: Confirm and delete the member\n" +
                                $"<N>: Cancel and return to the main menu\n"
                            );

                        confirmation_input = Console.ReadKey();
                        switch (confirmation_input.Key)
                        {
                            case ConsoleKey.Y:
                                UserInterface.Clear_screen();
                                list_of_members.RemoveAt(ix_for_removal);
                                success_flag = true;
                                loop_control_flag = false;
                                break;

                            default:
                                success_flag = false;
                                loop_control_flag = false;
                                break;
                        }
                    }
                    //if there is no matching id
                    else
                    {
                        UserInterface.Clear_screen();
                        UserInterface.Display_message($"There is no member with the ID \"{user_input}\"\n");
                        UserInterface.Display_message("Press <Y> to type a new Member ID or\nPress <X> to return to the main menu.");
                        confirmation_input = Console.ReadKey();
                        switch (confirmation_input.Key)
                        {
                            case ConsoleKey.Y:
                                //continue the loop, do nothing
                                break;
                            default:
                                success_flag = false;
                                loop_control_flag = false;
                                break;
                        }
                    }
                }
            }

            return success_flag;
        }



        /*
         * METHOD       : Load_From_File
         * 
         * DESCRIPTION  : opens the file streams and tries to parse and load a list of members from the file
         *              : OVERWRITES EXISTING MEMBER DATA
         * 
         * PARAMETERS   : string file_name: The name of the file that the list of members will be loaded from
         *              : List<Member> list_of_members: the list of objects that holds all the members
         * 
         * RETURNS      : Boolean success_flag: a true/false flag to indicate if the operation was successful
         */
        internal Boolean Load_From_File(string file_name, List<Member> list_of_members)
        {
            Boolean success_flag;

            FileStream my_file_stream;
            StreamReader my_stream_reader;

            //try to open and parse
            try
            {
                //Adapted code from this query: https://stackoverflow.com/questions/858756/how-to-parse-a-text-file-with-c-sharp
                //open streams
                my_file_stream = File.OpenRead(file_name);
                my_stream_reader = new StreamReader(my_file_stream);

                string? line; //a given line in the file
                Guid id;
                DateOnly dob;

                //parse the member data, line by line
                list_of_members.Clear();
                while ((line = my_stream_reader.ReadLine()) != null) //until the end of the file
                {

                    //separate the entire line of data into strings separated by '|' pipes (the streamreader already checks for line endings above)
                    string[] info_blocks = line.Split('|');

                    //now that we have the line's (ie.e member's) properties as strings in an array, we can convert them and add them to the list
                    Guid.TryParse(info_blocks[0], out id); //1st bloxk is an id, needs to be parsed
                    DateOnly.TryParse(info_blocks[4], out dob); //5th block is a DOB, needs to be parsed

                    //add the new member
                    list_of_members.Add(new Member());
                    //get the latest member index
                    int ix_lastmem = list_of_members.Count - 1;

                    list_of_members[ix_lastmem].Member_id = id;
                    list_of_members[ix_lastmem].Member_first_name = info_blocks[1];
                    list_of_members[ix_lastmem].Member_last_name = info_blocks[2];
                    list_of_members[ix_lastmem].Member_email = info_blocks[3];
                    list_of_members[ix_lastmem].Member_dob = dob;
                }

                //close streams when finished
                my_stream_reader.Close();
                my_file_stream.Close();
                success_flag = true; //flag is true to indicate a successful save
            }
            //catch all exceptions
            catch (Exception ex)
            {
                //ex itself passes a very detailed error message. Opting for ex.Message, which passes a much shorter error message.
                UserInterface.Display_error_message
                (
                    "An error has occurred while loading the data from the file. The data was not loaded.\n" + "Error information: \"" + ex.Message + "\"",
                    "Press any key to return to the menu..."
                );
                success_flag = false; //flag is false to indicate an error occurred
            }
            finally
            {
                UserInterface.Clear_screen();
            }

            return success_flag;
        }


        /*
         * METHOD       : Find_Loadable_File
         * 
         * DESCRIPTION  : Asks the user to input a file name (or path and file name) from which the list of members will be loaded
         *              : EXISTING MEMBERS WILL BE OVERWRITTEN
         * 
         * PARAMETERS   : List<Member> list_of_members: the list of objects that holds all the members
         * 
         * RETURNS      : NOTHING
         */
        internal void Find_Loadable_File(List<Member> list_of_members)
        {
            ConsoleKeyInfo confirmation_input;
            Boolean invalid_file_name = true; //flag for a while loop
            string? file_name; //? allows null in order for validation to be done by the code instead of an exception or warning being thrown

            while (invalid_file_name)
            {
                UserInterface.Clear_screen();

                UserInterface.Display_message
                    (
                        "Enter the name of the file from which to load a list of members." +
                        "\nNB:Be sure to specify file paths or extensions as needed:\n"
                    );
                file_name = Console.ReadLine();

                //Preventing null or blank filenames.
                //Exceptions around filehandling will be handled in a different method
                if (string.IsNullOrWhiteSpace(file_name))
                {
                    UserInterface.Display_error_message
                        (
                            "The file name may not be blank.",
                            "Press any key to enter a different file name."
                        );
                }
                //if filename is not blank
                else
                { 
                    if (File.Exists(file_name)) 
                    {
                        if (list_of_members.Count > 0)
                        {
                            UserInterface.Display_message
                                (
                                    $"Member data currently already exists. You are about to overwite this with data from \"{file_name}\".\n\n" +
                                    $"***ARE YOU SURE YOU WOULD LIKE TO OVERWRITE {list_of_members.Count} EXISTING MEMBERS?***\n" +
                                    $"<Y>: Confirm and overwrite\n" +
                                    $"<N>: Cancel and return to the main menu\n"
                                );

                            confirmation_input = Console.ReadKey();
                            if (confirmation_input.Key == ConsoleKey.Y)
                            {
                                //stay on track to overwrite the data
                                UserInterface.Clear_screen();
                            }
                            // i.e. if input is not Y or y
                            else
                            {
                                //get out of the while loop! (skip all remaining code in the while loop
                                invalid_file_name = false;
                                continue;
                            }
                        }
                        //if there are no members already,
                        else
                        {
                            //stay on track to try and load data
                        }

                        //if the file loading is successful
                        if (Load_From_File(file_name, list_of_members))
                        {
                            //exit the loop by changing the flag
                            invalid_file_name = false;

                            UserInterface.Display_message($"\nLoaded members successfully from {file_name}.");
                            UserInterface.Block_program("Press any key to continue...");
                        }
                        //else, i.e. if the file loading is unsuccessful
                        else
                        {
                            //if the loading is unsuccessful, error handling and messages will be displayed by Load_From_File()
                            //return to the main menu
                            invalid_file_name=false; //exit the loop
                            continue;
                        }
                    }
                    else
                    {
                        //if file doesn't exist, can't load the data
                        UserInterface.Clear_screen();
                        UserInterface.Display_message($"Could not find the file \"{file_name}\". Check the file path and\n");
                        UserInterface.Display_message("Press <Y> to try a different file name or\nPress <X> to return to the main menu.");

                        confirmation_input = Console.ReadKey();
                        if(confirmation_input.Key == ConsoleKey.Y)
                        {
                            //reloop without trying to load the file (it doesn't exist)
                            invalid_file_name = true;
                            continue;
                        }
                        else
                        {
                            invalid_file_name = false;
                            continue;
                        }

                    }                   

                }
            
            }
            return;
        }



    }
}
