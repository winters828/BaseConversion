using Microsoft.VisualBasic;
using System;
using System.Threading;

/*
    Needs to be fixed
        FIXED - After more than one conversion with the loop it doesn't take in the second base number
        I remember something about needing a filler int for the intake. 
            Solution: apparently using just .read() with char leaves a newline (\n) in the buffer,
                      I used string instead with readline and it worked just fine. Seems to clear the buffer.

        - BE SURE TO ADD A LIMIT TO THE LENGTH OF THE INPUT UP TO 16 PLACES  work on size restrictions.
            Using the highest value (f) 17 times converted to the most lengthy base case of binary will cause
            a stack overflow, however! 16 is ok and seems to be the line, upper bound if you will. 
            therefore, make 16 digits the highest number you can enter. 
            
            This does affect the fact that you can only enter a binary up to this point too and we could fix this by making the 
            input.length that you can enter different depending on base, but I don't think it's necessary to go into that much detail.

        - Do a bunch of test cases, keeping track of them
            Found: 0 by itself returns nothing, a quick if statement should fix that.
 */
namespace BaseConversion
{
    class baseconversion
    {
        static bool someError = false;

        static void Main(string[] args)
        {
            
            while(true) //The loop that repeats the program and lets the user convert again.
            {
                //The conversion
                Console.WriteLine(Conversion());

                //Asking the user
                Console.WriteLine("Would you like to convert another number? Y/N");
                string again = Console.ReadLine();

                if((again == "Y") || (again == "y"))
                {
                    Console.WriteLine("\n\nGreat! restarting...");
                    Console.Clear(); //Maybe we can keep the prior conversion in case the user wants to keep them

                    continue;

                }else if((again == "N") || (again == "n"))
                {
                    Console.WriteLine("\n\nHope I've been helpful!");
                    break;
                }else //If anything dumb...
                {
                    Console.WriteLine("\n\nPlease follow the instructions.");
                    break;
                }

            }//End of while loop


		}//End of Main





        public static string Conversion()
        {

            //Radix is essentially the base number | b10n = base 10 number
            int digit = 0;
            UInt64 radixto = 3, radixfrom = 0, b10n = 0, remainder = 0;
            string input,output="";
            char charRem;

            //All user input
            Console.WriteLine("Please enter the number you want converted. Don't exceed 16 digits");
            input = Console.ReadLine();         //Some error checking not in the error checking section...
            if (input.Length > 16) { return "Please follow instructions, no more than 16 digits"; } //This could also go in numberBaseCheck() but it's small so eh.
            Console.WriteLine("Please enter the base you would like the number converted from...");
            radixfrom = Convert.ToUInt64(Console.ReadLine());
            Console.WriteLine("Please enter the base you would like the number converted to...");
            radixto = Convert.ToUInt64(Console.ReadLine());


            //Error checking portion
            if (input == "0")
                return "Finished conversion: 0";

            Console.WriteLine(numberBaseCheck(input, radixfrom, radixto));
            if (someError)
                return "Please try again...";


            //Converting any other base to base 10 (including base 10)
            int exponent = input.Length-1; //We need the exponent to work down from highest exponent
            for(int i=0;i<input.Length;i++) //Meanwhile, we need the digits to work up from left most to right most number
            {
                switch(input[i])
                {
                    case 'a':
                    case 'A':
                        digit = 10;
                        break;
                    case 'b':
                    case 'B':
                        digit = 11;
                        break;
                    case 'c':
                    case 'C':
                        digit = 12;
                        break;
                    case 'd':
                    case 'D':
                        digit = 13;
                        break;
                    case 'e':
                    case 'E':
                        digit = 14;
                        break;
                    case 'f':
                    case 'F':
                        digit = 15;
                        break;
                    default:
                        digit = (int)Char.GetNumericValue(input[i]);
                        break;

                }// End of switch

                b10n = b10n + (Convert.ToUInt64(Math.Pow(radixfrom, exponent) * digit)); //Math.pow returns a double
                exponent--;
            }// End of conversion of any base to base 10

            //To see any number converted to base ten before the next step
            //Console.WriteLine("\n\nThis should be any other base number converted to base ten --> " + b10n);

            //Converting base 10 into any other base
            while(b10n!=0) 
            {
                //The remainder put together backwards is going to be your converted number
                remainder = b10n % radixto;
                b10n = b10n / radixto;

                //Accounting for letters to append to output
                switch(remainder)
                {
                    case 10:
                        charRem = 'A';
                        break;
                    case 11:
                        charRem = 'B';
                        break;
                    case 12:
                        charRem = 'C';
                        break;
                    case 13:
                        charRem = 'D';
                        break;
                    case 14:
                        charRem = 'E';
                        break;
                    case 15:
                        charRem = 'F';
                        break;
                    default:
                        charRem = remainder.ToString()[0];
                        break;

                }// End of switch
                output = charRem + output;
            }// End of while that goes from base 10 to any other base

              return "Finished conversion: " + output;
        }//End of conversion function.

 

        public static string numberBaseCheck(string input, UInt64 radixfrom, UInt64 radixto)
        {
            //true = error found | false = no error found
            bool numError = false, radixError = false; //Assume there's no errors 

            for (int i = 0; i < input.Length; i++) //Checking each character for the length of the string
            {//Number check. Ex: you wouldn't be converting from binary if your input was 99999. That doesn't make sense. Go through a check.

                switch (radixfrom)
                {
                    case 2:
                        if ((input[i] != '1') && (input[i] != '0'))
                        {
                            numError = true;
                        }
                        break;

                    case 3:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '0'))
                        {
                            numError = true;
                        }
                        break;

                    case 4:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '0'))
                        {
                            numError = true;
                        }
                        break;

                    case 5:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '4')
                             && (input[i] != '0'))
                        {
                            numError = true;
                        }
                        break;

                    case 6:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '4')
                             && (input[i] != '5') && (input[i] != '0'))
                        {
                            numError = true;
                        }
                        break;

                    case 7:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '4')
                             && (input[i] != '5') && (input[i] != '6') && (input[i] != '0'))
                        {
                            numError = true;
                        }
                        break;

                    case 8:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '4')
                             && (input[i] != '5') && (input[i] != '6') && (input[i] != '7') && (input[i] != '0'))
                        {
                            numError = true;
                        }
                        break;

                    case 9:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '4')
                             && (input[i] != '5') && (input[i] != '6') && (input[i] != '7') && (input[i] != '8')
                              && (input[i] != '0'))
                        {
                            numError = true;
                        }
                        break;

                    case 10:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '4')
                             && (input[i] != '5') && (input[i] != '6') && (input[i] != '7') && (input[i] != '8')
                              && (input[i] != '9') && (input[i] != '0'))
                        {
                            numError = true;
                        }
                        break;

                    case 11:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '4')
                             && (input[i] != '5') && (input[i] != '6') && (input[i] != '7') && (input[i] != '8')
                              && (input[i] != '9') && (input[i] != '0') && (input[i] != 'A') && (input[i] != 'a'))
                        {
                            numError = true;
                        }
                        break;

                    case 12:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '4')
                             && (input[i] != '5') && (input[i] != '6') && (input[i] != '7') && (input[i] != '8')
                              && (input[i] != '9') && (input[i] != '0') && (input[i] != 'A') && (input[i] != 'a')
                               && (input[i] != 'B') && (input[i] != 'b'))
                        {
                            numError = true;
                        }
                        break;

                    case 13:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '4')
                             && (input[i] != '5') && (input[i] != '6') && (input[i] != '7') && (input[i] != '8')
                              && (input[i] != '9') && (input[i] != '0') && (input[i] != 'A') && (input[i] != 'a')
                               && (input[i] != 'B') && (input[i] != 'b') && (input[i] != 'C') && (input[i] != 'c'))
                        {
                            numError = true;
                        }
                        break;

                    case 14:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '4')
                             && (input[i] != '5') && (input[i] != '6') && (input[i] != '7') && (input[i] != '8')
                              && (input[i] != '9') && (input[i] != '0') && (input[i] != 'A') && (input[i] != 'a')
                               && (input[i] != 'B') && (input[i] != 'b') && (input[i] != 'C') && (input[i] != 'c')
                                && (input[i] != 'D') && (input[i] != 'd'))
                        {
                            numError = true;
                        }
                        break;

                    case 15:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '4')
                             && (input[i] != '5') && (input[i] != '6') && (input[i] != '7') && (input[i] != '8')
                              && (input[i] != '9') && (input[i] != '0') && (input[i] != 'A') && (input[i] != 'a')
                               && (input[i] != 'B') && (input[i] != 'b') && (input[i] != 'C') && (input[i] != 'c')
                                && (input[i] != 'D') && (input[i] != 'd') && (input[i] != 'E') && (input[i] != 'e'))
                        {
                            numError = true;
                        }
                        break;

                    case 16:
                        if ((input[i] != '1') && (input[i] != '2') && (input[i] != '3') && (input[i] != '4')
                             && (input[i] != '5') && (input[i] != '6') && (input[i] != '7') && (input[i] != '8')
                              && (input[i] != '9') && (input[i] != '0') && (input[i] != 'A') && (input[i] != 'a')
                               && (input[i] != 'B') && (input[i] != 'b') && (input[i] != 'C') && (input[i] != 'c')
                                && (input[i] != 'D') && (input[i] != 'd') && (input[i] != 'E') && (input[i] != 'e')
                                 && (input[i] != 'F') && (input[i] != 'f'))
                        {
                            numError = true;
                        }
                        break;


                }//End of switch.
            }//End of for loop, so essentially the check.

            //Doing a radix check
            if (!(((radixfrom >= 2) && (radixfrom <= 16)) && ((radixto >= 2) && (radixto <= 16))))
            {
                radixError = true;
            }

            //Checking which errors occured 
            if((radixError) && (numError))
            {
                someError = true;
                return "Both the base and number entered have errors!";
            }else if((radixError) || (numError))
            {
                someError = true;
                return "Either the base or the number entered has an error!";
            }else //If both are false then there are no errors
            {
                someError = false; //In case we come through again
                return ""; //We don't need to tell the user our validation checked out, just if it doesn't.
                //return "Check finished! all set to convert =)";
            }


        }//End of numberValidation

    }//End of Class baseConversion
}
