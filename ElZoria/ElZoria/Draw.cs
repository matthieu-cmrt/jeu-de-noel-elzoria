using System;
using System.Security.AccessControl;

namespace Draw
{
    internal class Program
    {
        public static void PrintString(string obj, int x, int y)
        {
            Console.SetCursorPosition(x,y);
            Console.Write(obj);
        }

        public static void PrintHumanMale(int x, int y)
        {
            PrintString("    &&&&&     ",x,y+3);
            PrintString("    | oo|     ",x,y+4);
            PrintString("    \\  /      ",x,y+5);
            PrintString("   _| |_      ",x,y+6);
            PrintString("  /|\\__|\\     ",x,y+7);
            PrintString(" |/|   |\\\\    ",x,y+8);
            PrintString(" |||   | [\\   ",x,y+9);
            PrintString(" ||| __| ]|   ",x,y+10);
            PrintString(" /¯\\| || /¯\\  ",x,y+11);
            PrintString(" \\_/| || \\_/  ",x,y+12);
            PrintString("    |7 |7     ",x,y+13);
            PrintString("    || ||     ",x,y+14);
            PrintString("    /\\ \\\\",x,y+15);
            PrintString("    ¯¯  ¯¯     ",x,y+16);
            
        }
        public static void PrintHumanFemale(int x, int y)
        {
            PrintString("    /リノノ    ",x,y+2);
            PrintString("  /ノリノノ)))リ",x,y+3);
            PrintString("  ノノ| oo|    ",x,y+4);
            PrintString("  /ノ\\  /     ",x,y+5);
            PrintString("   _| |_      ",x,y+6);
            PrintString("  /|\\__|\\     ",x,y+7);
            PrintString(" |/|   |\\\\    ",x,y+8);
            PrintString(" |||   | [\\   ",x,y+9);
            PrintString(" ||| __| ]|   ",x,y+10);
            PrintString(" /¯\\| || /¯\\  ",x,y+11);
            PrintString(" \\_/| || \\_/  ",x,y+12);
            PrintString("    |7 |7     ",x,y+13);
            PrintString("    || ||     ",x,y+14);
            PrintString("    /\\ \\\\",x,y+15);
            PrintString("    ¯¯  ¯¯     ",x,y+16);
        }
        
        public static void PrintElfMale(int x, int y)
        {
            PrintString("  |\\&&&&&     ",x,y+3);
            PrintString("   \\| oo|     ",x,y+4);
            PrintString("    \\  /      ",x,y+5);
            PrintString("   _| |_      ",x,y+6);
            PrintString("  /|\\__|\\     ",x,y+7);
            PrintString(" |/|   |\\\\    ",x,y+8);
            PrintString(" |||   | [\\   ",x,y+9);
            PrintString(" ||| __| ]|   ",x,y+10);
            PrintString(" /¯\\| || /¯\\  ",x,y+11);
            PrintString(" \\_/| || \\_/  ",x,y+12);
            PrintString("    |7 |7     ",x,y+13);
            PrintString("    || ||     ",x,y+14);
            PrintString("    /\\ \\\\ ",x,y+15);
            PrintString("    ¯¯  ¯¯     ",x,y+16);
            
        }
        
        public static void PrintElfFemale(int x, int y)
        {
            PrintString("    /リノノ    ",x,y+2);
            PrintString("  |\\ノノ)))リ",x,y+3);
            PrintString(" /ノ\\| oo|    ",x,y+4);
            PrintString(" /ノノ\\  /     ",x,y+5);
            PrintString("  ノ_| |_      ",x,y+6);
            PrintString("  /|\\__|\\     ",x,y+7);
            PrintString(" |/|   |\\\\    ",x,y+8);
            PrintString(" |||   | [\\   ",x,y+9);
            PrintString(" ||| __| ]|   ",x,y+10);
            PrintString(" /¯\\| || /¯\\  ",x,y+11);
            PrintString(" \\_/| || \\_/  ",x,y+12);
            PrintString("    |7 |7     ",x,y+13);
            PrintString("    || ||     ",x,y+14);
            PrintString("    /\\ \\\\",x,y+15);
            PrintString("    ¯¯  ¯¯     ",x,y+16);
        }

        public static void PrintDwarfMale(int x, int y)
        {
            PrintString("    &&&&&     ", x, y + 5);
            PrintString("    | oo|     ", x, y + 6);
            PrintString("    \\  /      ", x, y + 7);
            PrintString("   _| |_      ", x, y + 8);
            PrintString("  /|\\__|\\     ", x, y + 9);
            PrintString(" |||   |[\\   ", x, y + 10);
            PrintString(" ||| __| ]|   ", x, y + 11);
            PrintString(" /¯\\| || /¯\\  ", x, y + 12);
            PrintString(" \\_/7 |7 \\_/  ", x, y + 13);
            PrintString("    || ||     ", x, y + 14);
            PrintString("    /\\ \\\\ ", x, y + 15);
            PrintString("    ¯¯  ¯¯     ", x, y + 16);
        }
        
        public static void PrintDwarfFemale(int x, int y)
        {
            PrintString("    /リノノ    ",x,y+4);
            PrintString("  /ノリノノ)))リ",x,y+5);
            PrintString("  ノノ| oo|    ",x,y+6);
            PrintString(" /ノ \\  /     ",x,y+7);
            PrintString("   _| |_      ",x,y+8);
            PrintString("  /|\\__|\\     ",x,y+9);
            PrintString(" |||   |[\\   ",x,y+10);
            PrintString(" ||| __| ]|   ",x,y+11);
            PrintString(" /¯\\| || /¯\\  ",x,y+12);
            PrintString(" \\_/7 |7 \\_/  ",x,y+13);
            PrintString("    || ||     ",x,y+14);
            PrintString("    /\\ \\\\  ",x,y+15);
            PrintString("    ¯¯  ¯¯     ",x,y+16);
        }
        
        public static void PrintMageMale(int x, int y)
        {
            PrintString("    ___",x,y+1);
            PrintString("    \\/ \\      ",x,y+2);
            PrintString("   _/___\\_  ",x,y+3);
            PrintString("    | oo|     ",x,y+4);
            PrintString("    \\  /      ",x,y+5);
            PrintString("   _| |_      ",x,y+6);
            PrintString("  /|\\__|\\     ",x,y+7);
            PrintString(" |/|   |\\\\    ",x,y+8);
            PrintString(" |||   | [\\   ",x,y+9);
            PrintString(" ||| __| ]|   ",x,y+10);
            PrintString(" /¯\\| || /¯\\  ",x,y+11);
            PrintString(" \\_/| || \\_/  ",x,y+12);
            PrintString("    |7 |7     ",x,y+13);
            PrintString("    || ||     ",x,y+14);
            PrintString("    /\\ \\\\ ",x,y+15);
            PrintString("    ¯¯  ¯¯    ",x,y+16);
        }
        
        public static void PrintMageFemale(int x, int y)
        {
            
            PrintString("    ___",x,y+1);
            PrintString("    \\/ \\      ",x,y+2);
            PrintString("   _/___\\_  ",x,y+3);
            PrintString("  ノノ| oo|     ",x,y+4);
            PrintString(" /ノノ\\  /      ",x,y+5);
            PrintString(" ノ _| |_      ",x,y+6);
            PrintString("  /|\\__|\\     ",x,y+7);
            PrintString(" |/|   |\\\\    ",x,y+8);
            PrintString(" |||   | [\\   ",x,y+9);
            PrintString(" ||| __| ]|   ",x,y+10);
            PrintString(" /¯\\| || /¯\\  ",x,y+11);
            PrintString(" \\_/| || \\_/  ",x,y+12);
            PrintString("    |7 |7     ",x,y+13);
            PrintString("    || ||     ",x,y+14);
            PrintString("    /\\ \\\\  ",x,y+15);
            PrintString("    ¯¯  ¯¯     ",x,y+16);
            
        }

        public static void Heart(int x, int y)
        {
            PrintString(" # #",x,y);
            PrintString("|&#&|",x,y+1);
            PrintString(" \\&/",x,y+2);
            PrintString("  #",x,y+3);
        }
        
        public static void Shield(int x, int y)
        {
            PrintString("#####",x,y);
            PrintString("|###|",x,y+1);
            PrintString("\\###/",x,y+2);
            PrintString(" \\#/",x,y+3);
        }
        
        public static void Sword(int x, int y)
        {
            PrintString("  ^",x,y);
            PrintString("  |",x,y+1);
            PrintString(" \\=/",x,y+2);
            PrintString("  %",x,y+3);
        }
        
        public static void Boots(int x, int y)
        {
            PrintString("mmm",x,y);
            PrintString("| |",x,y+1);
            PrintString("| |_",x,y+2);
            PrintString("|___)",x,y+3);
        }
        
        public static void Genie(int x, int y)
        {
            PrintString(" |¯|",x,y);
            PrintString(" | |",x,y+1);
            PrintString("/   \\",x,y+2);
            PrintString("¯¯¯¯¯",x,y+3);
        }
        
        public static void Bag(int x, int y)
        {
            PrintString(" \\/",x,y+1);
            PrintString(" /\\",x,y+2);
            PrintString(" \\/",x,y+3);
        }
        
        public static void BigBag(int x, int y)
        {
            PrintString(" _\\/_",x,y);
            PrintString("/   |",x,y+1);
            PrintString("|   |",x,y+2);
            PrintString("\\___/",x,y+3);
        }

        public static void Demon(int x, int y) //boss secret (gain force, def, vie, agilité, intelligence)
        {
            y++;
            x += 10;
            PrintString("              v",x,y+3);
            PrintString("        (__)v | v",x,y+4);
            PrintString("        /\\/\\\\_|_/",x,y+5);
            PrintString("       _\\__/  |",x,y+6);
            PrintString("      /  \\/`\\<`)",x,y+7);
            PrintString("      \\ (  |\\_/",x,y+8);
            PrintString("      /)))-(  |",x,y+9);
            PrintString("     / /^ ^ \\ |",x,y+10);
            PrintString("    /  )^/\\^( |",x,y+11);
            PrintString("    )_//`__>> |",x,y+12);
            PrintString("      #   #`  |", x, y + 13);
        }

        public static void Gost(int x, int y) //zone secrete ?
        {
            PrintString("       .-.",x,y+4);
            PrintString("      (* *)",x,y+5);
            PrintString("   /\\_.' '._/\\",x,y+6);
            PrintString("   |         |",x,y+7);
            PrintString("    \\       /",x,y+8);
            PrintString("     \\    /`",x,y+9);
            PrintString("   (__)  /",x,y+10);
            PrintString("   `.__.'",x,y+11);
        }

        public static void Sirene(int x, int y) //zone secrete ?
        {
            PrintString("  ,;:;,        /^\\   /^\\",x,y+5);
            PrintString(" ((@ @))       \\\\\\\\.////",x,y+6);
            PrintString(" ))\\=/((        \\\\\\|///",x,y+7);
            PrintString(".((/^\\))         \\\\    |//",x,y+8);
            PrintString("//)W)(W(``''--.._/`'/",x,y+9);
            PrintString("`m(\\,_))__________.'",x,y+10);
        }

        public static void Slime(int x, int y) //plaine
        {
            x += 3;
            PrintString("     .....",x,y+3);
            PrintString("  ...  _  ...",x,y+4);
            PrintString(" .    {O}    .",x,y+5);
            PrintString(".      T      .",x,y+6);
            PrintString(" .............",x,y+7);
        }

        public static void Goblin(int x, int y) //grotte
        {
            x += 4;
            y+=2;
            PrintString("/¯¯\\/¯¯¯\\/¯¯\\",x,y);
            PrintString("¯¯\\| * * |/¯¯",x,y+1);
            PrintString("    \\_^_/",x,y+2);
            PrintString("    _| |_   \\-/",x,y+3);
            PrintString("   /|\\__|\\  //",x,y+4);
            PrintString("  |/|   |\\\\//",x,y+5);
            PrintString("  ||| __| ~'",x,y+6);
            PrintString("  /_\\| ||",x,y+7);
            PrintString("  \\_/7 |7",x,y+8);
            PrintString("    || ||",x,y+9);
            PrintString("    /\\ \\\\",x,y+10);
            PrintString("    ¯¯  ¯¯",x,y+11);
        }

        public static void Wolf(int x, int y) //foret
        {
            y++;
            PrintString("                  .",x,y);
            PrintString("                 / V\\",x,y+1);
            PrintString("              / `  /",x,y+2);
            PrintString("              <<   |",x,y+3);
            PrintString("              /    |",x,y+4);
            PrintString("            /      |",x,y+5);
            PrintString("          /        |",x,y+6);
            PrintString("         /    \\  \\ /",x,y+7);
            PrintString("        (      ) | |",x,y+8);
            PrintString("  ______|   _/_  | |",x,y+9);
            PrintString("<_______\\______)\\__)",x,y+10);
        }

        public static void Sheep(int x, int y) //plaine
        {
            x += 3;
            PrintString("┌-/7",x,y+3);
            PrintString("(oo)/¯\\/¯\\/¯\\",x,y+4);
            PrintString("(..)         )",x,y+5);
            PrintString("    \\_/\\_/\\_/",x,y+6);
            PrintString("     ||    ||",x,y+7);
            PrintString("     ¯¯    ¯¯",x,y+8);
            
        }

        public static void Rabbit(int x, int y) //foret
        {
            x += 5;
            PrintString("(\\_/)",x,y+5);
            PrintString("( *,*)",x,y+6);
            PrintString("(')_(')",x,y+7);
        }

        public static void Fairy(int x, int y) //foret assez rare, necessite une bouteille
        {
            PrintString("   (\\{\\",x,y);
            PrintString("   { { \\ ,~,",x,y+1);
            PrintString("  { {   \\)))  *",x,y+2);
            PrintString("   { {  (((  /",x,y+3);
            PrintString("    {/{/; ,\\/",x,y+4);
            PrintString("       (( '",x,y+5);
            PrintString("        \\` \\",x,y+6);
            PrintString("        (/  \\",x,y+7);
            PrintString("        `)  `\\",x,y+8);
        }

        public static void Phoenix(int x, int y) //grotte niv 10
        {
            PrintString("      __             __",x,y);
            PrintString("   .-'.'     .-.     '.'-.",x,y+1);
            PrintString(" .'.((      ( ^ `>     )).'.",x,y+2);
            PrintString("/`'- \\'._____\\ (_____.'/ -'`\\",x,y+3);
            PrintString("|-''`.'------' '------'.`''-|",x,y+4);
            PrintString("|.-'`.'.'.`/ | | \\`.'.'.`'-.|",x,y+5);
            PrintString(" \\ .' . /  | | | |  \\ . '. /",x,y+6);
            PrintString("  '._. :  _|_| |_|_  : ._.'",x,y+7);
            PrintString("     ````` /T\"Y\"T\\ `````",x,y+8);
            PrintString("          / | | | \\",x,y+9);
            PrintString("         `'`'`'`'`'`",x,y+10);
        }

        public static void Cat(int x, int y) //tour sorciere
        {
            PrintString("   |\\---/|",x,y);
            PrintString("   | ,_, |",x,y+1);
            PrintString("    \\_`_/-..----.",x,y+2);
            PrintString(" ___/ `   ' ,\"\"+ \\",x,y+3);
            PrintString("(__...'   __\\    |`.___.';",x,y+4);
            PrintString("  (_,...'(_,.`__)/'.....+",x,y+5);
        }
        
        public static void Dragon(int x, int y) // boss de fin
        {
            y++;
            PrintString(" <>=======()",x,y+1);
            PrintString("(/\\___   /|\\\\          ()==========<>_",x,y+2);
            PrintString("      \\_/ | \\\\        //|\\   ______/ \\)",x,y+3);
            PrintString("        \\_|  \\\\      // | \\_/",x,y+4);
            PrintString("          \\|\\/|\\_   //  /\\/",x,y+5);
            PrintString("           (oo)\\ \\_//  /",x,y+6);
            PrintString("          //_/\\_\\/ /  |",x,y+7);
            PrintString("         @@/  |=\\  \\  |",x,y+8);
            PrintString("              \\_=\\_ \\ |",x,y+9);
            PrintString("                \\==\\ \\|\\_",x,y+10);
            PrintString("             __(\\===\\(  )\\",x,y+11);
            PrintString("            (((~) __(_/   |",x,y+12);
            PrintString("                 (((~) \\  /",x,y+13);
            PrintString("                 ______/ /",x,y+14);
            PrintString("                 '------'",x,y+15);
        }
        
        public static void Witch(int x, int y) //boss tour
        {
            x += 12;
            PrintString("      ±———±",x,y+5);
            PrintString("     /_/   \\",x,y+6);
            PrintString("      /_/  _\\_",x,y+7);
            PrintString("     /________>",x,y+8);
            PrintString("     /ノリノノ)))リ",x,y+9);
            PrintString("    ノノ(!´U`ノノ'",x,y+10);
            PrintString("   '((/)iMiつ",x,y+11);
            PrintString("#I)—{</_|ノ_)7——•",x,y+12);
            PrintString("        し'ノ",x,y+13);
        }
        
        public static void Skeleton(int x, int y) // boss plaine
        {
            y++;
            x += 13;
            PrintString("    ,--.",x,y+3);
            PrintString("   ([ oo]",x,y+4);
            PrintString("    `- ^\\",x,y+5);
            PrintString("  _  I`-'",x,y+6);
            PrintString(",o(`-V'",x,y+7);
            PrintString("|( `-H-'",x,y+8);
            PrintString("|(`--A-'",x,y+9);
            PrintString("|(`-/_\\'\\",x,y+10);
            PrintString("O `'I ``\\\\",x,y+11);
            PrintString("(\\  I    |\\,",x,y+12);
            PrintString(" \\\\-T-'`, |H",x,y+13);
        }

        public static void Ents(int x, int y)
        {
            y += 3;
            x += 9;
            PrintString("        __    __",x,y);
            PrintString("     __/  \\__/  \\__",x,y+1);
            PrintString("    /    \\     /   \\",x,y+2);
            PrintString("   |     o     o    |",x,y+3);
            PrintString("   |                |",x,y+4);
            PrintString("   |     vvvvvv     |",x,y+5);
            PrintString("   \\___ __^^^^__ ___/",x,y+6);
            PrintString("       \\_ \\  / _/",x,y+7);
            PrintString("         \\_||_/",x,y+8);
            PrintString("          |¯¯|",x,y+9);
            PrintString("          |  |",x,y+10);
            PrintString("          /  \\",x,y+11);
            PrintString("          ¯¯¯¯",x,y+12);
        }

        public static void Colosse(int x, int y)
        {
            y += 4;
            x += 10;
            PrintString(" /¯\\    ___    /¯\\",x,y);
            PrintString(" \\_/   /\\ /\\   \\_/",x,y+1);
            PrintString(" | |   |* *|   | |",x,y+2);
            PrintString(" \\ \\__/¯\\A/¯\\__/ /",x,y+3);
            PrintString("  \\_     ¯     _/",x,y+4);
            PrintString("   |           |",x,y+5);
            PrintString("   |           |",x,y+6);
            PrintString("   \\__       __/",x,y+7);
            PrintString("   / |       | \\",x,y+8);
            PrintString("   | \\_______/ |",x,y+9);
            PrintString("  /¯ |       | ¯\\",x,y+10);
            PrintString("  ¯¯¯¯       ¯¯¯¯",x,y+11);
        }
            
    }
        
}