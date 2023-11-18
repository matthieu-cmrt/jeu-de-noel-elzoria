using System;
using System.Data;
using System.Reflection;
using System.Xml.Schema;

namespace Affichage
{
    internal class Program
    {

        public static void testdefonc()
        {
        }
        public static void PrintDebut(string sexe)
        {
            int x = 20;
            int y = 4;
            Console.SetCursorPosition(x, y);
            if (sexe == "♀")
            {
                Console.Write("1. Humain");
                Draw.Program.PrintHumanFemale(x, y + 1);
                x += 14;
                Console.SetCursorPosition(x, y);
                Console.Write("2. Elfe");
                Draw.Program.PrintElfFemale(x, y + 1);
                x += 14;
                Console.SetCursorPosition(x, y);
                Console.Write("3. Nain");
                Draw.Program.PrintDwarfFemale(x, y + 1);
                x += 14;
                Console.SetCursorPosition(x, y);
                Console.Write("4. Mage");
                Draw.Program.PrintMageFemale(x, y + 1);
            }
            else
            {
                Console.Write("1. Humain");
                Draw.Program.PrintHumanMale(x, y + 1);
                x += 14;
                Console.SetCursorPosition(x, y);
                Console.Write("2. Elfe");
                Draw.Program.PrintElfMale(x, y + 1);
                x += 14;
                Console.SetCursorPosition(x, y);
                Console.Write("3. Nain");
                Draw.Program.PrintDwarfMale(x, y + 1);
                x += 14;
                Console.SetCursorPosition(x, y);
                Console.Write("4. Mage");
                Draw.Program.PrintMageMale(x, y + 1);
            }

        }

        public static void Selec(string race, string sexe, int x, int y)
        {
            if (sexe == "♀")
            {
                if (race == "Humain")
                    Draw.Program.PrintHumanFemale(x, y);
                else if (race == "Elfe")
                    Draw.Program.PrintElfFemale(x, y);
                else if (race == "Nain")
                    Draw.Program.PrintDwarfFemale(x, y);
                else
                    Draw.Program.PrintMageFemale(x, y);
            }
            else
            {
                if (race == "Humain")
                    Draw.Program.PrintHumanMale(x, y);
                else if (race == "Elfe")
                    Draw.Program.PrintElfMale(x, y);
                else if (race == "Nain")
                    Draw.Program.PrintDwarfMale(x, y);
                else
                    Draw.Program.PrintMageMale(x, y);
            }
        }

        public static void Statsselec(string statname, int[] stats, int[] max, int x, int y)
        {
            Console.SetCursorPosition(x + 5, y + 1);
            Console.Write(" " + statname + ":");
            Console.SetCursorPosition(x + 5, y + 2);
            if (statname == "Vie")
            {
                Console.Write(" " + stats[0] + " / " + max[0]);
                Draw.Program.Heart(x, y);
            }
            else if (statname == "Defense")
            {
                Console.Write(" " + stats[1]);
                Draw.Program.Shield(x, y);
            }
            else if (statname == "Attaque")
            {
                Console.Write(" " + stats[2]);
                Draw.Program.Sword(x, y);
            }
            else if (statname == "Agilité")
            {
                Console.Write(" " + stats[4]);
                Draw.Program.Boots(x, y);
            }
            else if (statname == "Intelligence")
            {
                Console.Write(" " + stats[3]);
                Draw.Program.Genie(x, y);
            }
            else
            {
                if (statname == "Sac")
                {
                    Console.Write(" " + stats[5] + " / " + max[1]);
                    Draw.Program.Bag(x, y);
                }
                else
                {
                    Console.Write(" " + stats[5] + " / " + max[1]);
                    Draw.Program.BigBag(x, y);
                }
            }

        }

        public static void Separation(int x, int y, int fin)
        {
            while (y > fin)
            {
                Console.SetCursorPosition(x, y - 1);
                Console.Write("|");
                y--;
            }

        }

        public static void Exp(int exp, int lvl, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("Niv." + lvl + " [");
            int av = (10*exp) / (10*lvl);
            for (int i = 0; i < 10; i++)
            {
                if (av > 0)
                {
                    Console.Write("#");
                    av--;
                }
                else
                    Console.Write(".");

            }

            Console.Write("]");
        }

        public static void Coin(int nb, int x, int y)
        {

            Console.SetCursorPosition(x, y);
            Console.Write("© " + nb);
        }

        public static void Interfacestats(string[] att, int[] stats, int[] max, int[] exp)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("{ Stats } Arme : "+att[3]);
            Console.WriteLine(att[0] + " " + att[2]);
            Console.SetCursorPosition(3, 2);
            Console.WriteLine(att[1]);
            Selec(att[1], att[0], 0, 2);
            Statsselec("Vie", stats, max, 15, 2);
            Statsselec("Defense", stats, max, 15, 8);
            Statsselec("Attaque", stats, max, 15, 14);
            Statsselec("Agilité", stats, max, 33, 2);
            Statsselec("Intelligence", stats, max, 33, 8);
            if (max[1] >= 150)
                Statsselec("Grand Sac", stats, max, 33, 14);
            else
                Statsselec("Sac", stats, max, 33, 14);
            Exp(exp[0], exp[1], 15, 19);
            Coin(stats[6], 45, 19);
            Console.SetCursorPosition(0, 20);
            Console.Write("------------------------------------------------------┼");
            Separation(54, 20, 0);
        }

        public static int Interfacesac(int[] max, int[] bagcount, string[] bag)
        {
            Console.SetCursorPosition(55, 21);
            Console.Write("{ ");
            if (max[1] >= 150)
                Console.Write("Grand Sac");
            else
                Console.Write("Sac");
            Console.Write(" }");
            int nb = Bag.Program.Show(bagcount, bag, 55, 23);
            Console.SetCursorPosition(54, 20);
            Console.Write("┼------------------------------------------------------┼");
            Console.SetCursorPosition(54, 40);
            Console.Write("└------------------------------------------------------┴");
            Separation(109, 40 ,21);
            Separation(54, 40 ,21);
            return nb;
        }
        
        public static void Interfacéquipe(int nb, string[] att, int[] stats, int[] max, int[] exp)
        {
            int x = nb * 55;
            Console.SetCursorPosition(x, 0);
            Console.Write("{ Stats Equipier "+nb+" } Arme : "+att[3]);
            Console.SetCursorPosition(x, 1);
            Console.Write(att[0] + " " + att[2]);
            Console.SetCursorPosition(x+3, 2);
            Console.Write(att[1]);
            Selec(att[1], att[0], x, 2);
            Statsselec("Vie", stats, max, x+15, 2);
            Statsselec("Defense", stats, max, x+15, 8);
            Statsselec("Attaque", stats, max, x+15, 14);
            Statsselec("Agilité", stats, max, x+32, 5);
            Statsselec("Intelligence", stats, max, x+32, 11);
            Exp(exp[0], exp[1], x+15, 19);
            Console.SetCursorPosition(x, 20);
            Console.Write("------------------------------------------------------┼");
            Separation(x+54, 20, 0);

        }

        public static void InterfaceRencontre(string[] att, int[] stats, int[] max, int[] exp)
        {
            int x = 55;
            Console.SetCursorPosition(x+1, 21);
            Console.Write(" { Rencontre } Arme : "+att[3]);
            Console.SetCursorPosition(x+1, 22);
            Console.Write(att[0] + " " + att[2]);
            Console.SetCursorPosition(x+4, 23);
            Console.Write(att[1]);
            Selec(att[1], att[0], x, 23);
            Statsselec("Vie", stats, max, x+15, 22);
            Statsselec("Defense", stats, max, x+15, 28);
            Statsselec("Attaque", stats, max, x+15, 34);
            Statsselec("Agilité", stats, max, x+32, 25);
            Statsselec("Intelligence", stats, max, x+32, 31);
            Exp(exp[0], exp[1], x+15, 39);
            Console.SetCursorPosition(x-1, 20);
            Console.Write("┼-----------------------------------------------------┼");
            Console.SetCursorPosition(x-1, 40);
            Console.Write("└-----------------------------------------------------┘");
            Separation(54, 40, 21);
            Separation(x+53, 40, 21);
        }
        public static void Interfaceshop(int[] prix, string[] obj, int[] quant)
        {
            Console.SetCursorPosition(111, 21);
            Console.WriteLine("{ Magasin }");
            
            int x = 114;
            int y = 23;
            Console.SetCursorPosition(x+2,y);
            Console.Write("Objet");
            Console.SetCursorPosition(x+25,y);
            Console.Write("Nb");
            Console.SetCursorPosition(x+29,y);
            Console.Write("Prix");
            y+=2;
            for (int i = 0; i < obj.Length; i++)
            {
                int nb = i + 1;
                Console.SetCursorPosition(x,y);
                Console.Write(nb+") "+obj[i]);
                Console.SetCursorPosition(x+25,y);
                Console.Write(quant[i]);
                Console.SetCursorPosition(x+29,y);
                Console.Write(prix[i]);
                y += 2;
            }
            Console.SetCursorPosition(110, 20);
            Console.Write("----------------------------------------┬");
            Console.SetCursorPosition(110, 40);
            Console.Write("----------------------------------------┘");
            Separation(150, 40 ,21);
        }
        
        public static void InterfaceMonstre(string name, int niv, int numéro)
        {
            int x = 54 + 27 * numéro;
            int y = 21;
            Console.SetCursorPosition( x+1, y);
            Console.WriteLine("  { "+name+" - Niv."+niv+" }");
            Monster.Program.ShowMonster(name,niv,x,y);
            Console.SetCursorPosition( x, 20);
            if (numéro==1)
                Console.Write("┬--------------------------┬");
            else
                Console.Write("┼--------------------------┬");
            Console.SetCursorPosition( x, 40);
            if (numéro==1)
                Console.Write("┴--------------------------┘");
            else    
                Console.Write("└--------------------------┘");
            Separation(x, 40 ,21);
            Separation(x+27, 40 ,21);

        }

        public static void InferfaceBoss(string name, int niv, int[] statboss)
        {
            int x = 54;
            int y = 21;
            Console.SetCursorPosition( x+1, y);
            Console.WriteLine("  { Boss : "+name+" - Niv."+niv+" }");
            Monster.Program.ShowBoss(name, statboss, niv,x,y);
            Console.SetCursorPosition( x, 20);
            Console.Write("┼------------------------------------------------------┴--------┐");
            Console.SetCursorPosition( x, 40);
            Console.Write("└---------------------------------------------------------------┘");
            Separation(x, 40 ,21);
            Separation(x+64, 40 ,21);
        }

        public static void InterfaceAide(int num)
        {
            if (num != 0)
            {
                int x = 118;
                int y = 21;
                string name = "";
                switch (num)
                {
                    case 1:
                        name ="Fée";
                        Draw.Program.Fairy(x+14,y+4);
                        break;
                    case 2:
                        name = "Chat";
                        Draw.Program.Cat(x+10,y+6);
                        break;
                    case 3:
                        Draw.Program.Phoenix(x+9,y+4);
                        name = "Phoenix";
                        break;
                }
                Console.SetCursorPosition( x+1, y);
                Console.WriteLine(" { Aide : "+name+" }");
                Console.SetCursorPosition( x, 20);
                Console.Write("┬---------------------------------------------┼");
                Console.SetCursorPosition( x, 40);
                Console.Write("├---------------------------------------------┘");
                Separation(x, 40 ,21);
                Separation(164, 40 ,21);
            }
        }
        
        

    }
}