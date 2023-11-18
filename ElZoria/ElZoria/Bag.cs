using System;
using System.Net.NetworkInformation;
using System.Security.Policy;

namespace Bag
{
    internal class Program
    {
        public static int Show(int[] bagcount, string[] bag,int x, int y)
        {
            int nb = 1;
            for (int i=0; i<bag.Length; i++)
            {
                if (bagcount[i] != 0)
                {
                    if (nb <= 16)
                    {
                        Console.SetCursorPosition(x+2,y);
                        Console.Write(nb+") "+bag[i]+" x"+bagcount[i]);
                    }
                    else
                    {
                        Console.SetCursorPosition(x+27,y);
                        Console.Write(nb+") "+bag[i]+" x"+bagcount[i]);
                    }
                    nb++;
                    y++;
                }
            }

            if (nb == 1)
            {
                Console.SetCursorPosition(x+19,y+7);
                Console.Write("** Sac Vide **");
            }

            return nb - 1;
        }

        public static void Add(int[] stats,int[] max, int[] bagcount, string[] bag, string obj, int nb)
        {
            int place = 0;
            for (int i = 0; obj != bag[i]; i++)
            {
                place++;
            }
            bagcount[place] += nb;
            ElZoria.Program.Addstat(stats,5,nb,max);
        }
        
        public static void Add2(int[] stats,int[] max, int[] bagcount, string[] bag, string obj, int nb)
        {
            int place = 0;
            for (int i = 0; obj != bag[i]; i++)
            {
                place++;
            }
            bagcount[place] += nb;
            
        }
        
        public static void Throw(int[] stats,int[] max, int[] bagcount, string[] bag, int obj, int nb)
        {
            string objet = Locate(bag, bagcount, obj);
            int place = 0;
            for (int i = 0; objet != bag[i]; i++)
            {
                place++;
            }

            if (bagcount[place] == nb)
            {
                bagcount[place] = 0;
                int x = -nb;
                ElZoria.Program.Addstat(stats,5,x,max);
                nb = 0;
            }
            else if (bagcount[place] < nb)
            {
                int x = bagcount[place]-nb;
                nb -= bagcount[place];
                ElZoria.Program.Addstat(stats,5,x,max);
                bagcount[place] = 0;
            }
            else
            {
                bagcount[place] -= nb;
                int x = -nb;
                ElZoria.Program.Addstat(stats,5,x,max);
                nb = 0;
            }
            
        }
        
        public static void Throw2(int[] stats,int[] max, int[] bagcount, string[] bag, int obj, int nb)
        {
            string objet = Locate(bag, bagcount, obj);
            int place = 0;
            for (int i = 0; objet != bag[i]; i++)
            {
                place++;
            }

            if (bagcount[place] == nb)
            {
                bagcount[place] = 0;
                int x = -nb;
                nb = 0;
            }
            else if (bagcount[place] < nb)
            {
                int x = bagcount[place]-nb;
                nb -= bagcount[place];
                bagcount[place] = 0;
            }
            else
            {
                bagcount[place] -= nb;
                int x = -nb;
                nb = 0;
            }
            
        }
        
        public static void Swap(int[] stats,int[] max, int[] bagcount, string[] bag, int obj1, string obj2, int nb) //swap nb obj1 for nb obj2
        {
            Throw2(stats, max, bagcount, bag, obj1, nb);
            Add2(stats,max, bagcount,bag,obj2, nb);
        }

        public static string Locate(string[] bag, int[] bagcount, int indice)
        {
            int i = 0;
            while (indice != 0)
            {
                if (bagcount[i] != 0)
                {
                    indice--;
                }

                i++;
            }
            return bag[i-1];
        }

        public static int LocatePlace(int[] bagcount, int indice)
        {
            int i = 0;
            while (indice != 0)
            {
                if (bagcount[i] != 0)
                {
                    indice--;
                }

                i++;
            }
            return i-1;
        }

    }
}