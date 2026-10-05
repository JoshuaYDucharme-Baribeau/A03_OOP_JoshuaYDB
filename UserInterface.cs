
namespace A03_OOP_JoshuaYDB
{
    //making this class static because it shouldn't manipulate anything.
    //because it only has helper functions, it doesn't need to exist as an instance
    static internal class UserInterface
    {


        //Recycled helper functions below this point

        /*
         * METHOD       : Display_menu
         * 
         * DESCRIPTION  : displays the main menu of the program
         * 
         * PARAMETERS   : NONE
         * 
         * RETURNS      : NOTHING
         */
        internal static void Display_menu()
        {
            Clear_screen(); //clear the screen before displaying anything
            //Main menu's WriteLine. Concatenated for code readability. Displays almost exactly as coded.
            Console.WriteLine
                (
                    "<A>dd a member\n" +
                    "<L>ist all members\n" +
                    "<D>elete a member\n" +
                    "<S>ave member data to a file\n" +
                    "L<O>ad members from a file" +
                    "E<X>it the program\n" +
                    "Please choose the operation you would like to perform:\n\n"
                );

            return;
        }


        /*
         * METHOD       : Clear_screen
         * 
         * DESCRIPTION  : clears the UI (i.e. clears the cli display).
         *                Although a method for this exists in the System namespace, 
         *                I use it frequently so I wanted my own method that I 
         *                could modify if I needed. In the end, I did not modify it.
         * 
         * PARAMETERS   : NONE
         * 
         * RETURNS      : NOTHING
         */
        internal static void Clear_screen()
        {
            Console.Clear();
            //no return needed for void return type
        }


        /*
         * METHOD       : Block_program
         * 
         * DESCRIPTION  : displays a message and awaits for key input before continuing
         * 
         * PARAMETERS   : string message_prompt : a string that will be written to the console.
         * 
         * RETURNS      : NOTHING
         */
        internal static void Block_program(string message_prompt = "Press any key to continue...") //sets a default param to make the param optional.
        {
            Console.WriteLine(message_prompt);
            Console.ReadKey();
        }


        /*
         * METHOD       : Display_error_message
         * 
         * DESCRIPTION  : displays an informative error message and calls a method to block the program
         * 
         * PARAMETERS   : string error_message  : [OPTIONAL] A specific error message that will be displayed. 
         *              : string message_prompt : [OPTIONAL] [shadows the parameter it passes itself to] A prompt that will be passed to Block_Program when it is called.
         * 
         * RETURNS      : NOTHING
         */
        internal static void Display_error_message(string error_message = "An error has occurred.", string message_prompt = "Press any key to try again...") //Two default params
        {
            Console.WriteLine("\n\n" + error_message);
            Block_program(message_prompt);
        }
    }
}
