using System;
using System.Globalization;

namespace Equipe
{
    internal class Program
    {
        public static void RencontreProba(ref int nbe, string[] atte1, int[] state1, int[] maxe1, int[] expe1, 
            string[] atte2, int[] state2, int[] maxe2, int[] expe2, int[] exp, string[] att, int[] stats)
        {
            Random rnd = new Random();
            int proba = rnd.Next(1, 7);
            if (proba == 2)
            {
                RencontreCrea(ref nbe, atte1, state1,maxe1,expe1,atte2,state2,maxe2,expe2,exp,att,stats);
            }
        }

        public static void StatRenc(string[] att, int[] exp, int[] stats, int[] max, int lvlmax)
        {
            Random rnd = new Random();
            int lvl = rnd.Next(1, lvlmax);
            exp[1] = lvl;
            for (int i = 2; i <= lvl; i++)
            {
                ElZoria.Program.PassNivStats(stats,att,max,i);
            }
        }
        
        public static void RencontreCrea(ref int nbe, string[] atte1, int[] state1, int[] maxe1, int[] expe1, 
            string[] atte2, int[] state2, int[] maxe2, int[] expe2, int[] exp,string[] att, int[] stats)
        {
            string[] atterenc = {"sexe","race","name","arme"};
            string[] arme = {"Epée du début","Arc du début","Hache du début","Sceptre du début"};
            int[] experenc = {0,1};
            int[] maxerenc = {40};
            int[] staterenc = {40, 0, 0, 0, 0};
            Random rnd = new Random();
            string[] races = {"Humain", "Elfe", "Nain", "Mage"};
            string[] malename = {"Aldric","Maxime","Kezian","Idaho","Fernando","Maximilien","Ethan","Rayan","Juan",
                "Ronan","Arthur","Thomas","Auguste","Henri","Sacha","Vincent","Nolan","Paul","François"};
            string[] femalename = {"Zoé","Marie","Ylan","Morgane","Amélie","Charlotte","Christine","Margaux","Salomé",
                "Eva","Cécile","Jeanne","Ava","Ameline","Romane","Maëlle","Alice","Louise","Karine"};
            int sexe = rnd.Next(1, 5);
            int nom= rnd.Next(0, 19);
            int race= rnd.Next(0, 4);
            atterenc[1] = races[race];
            atterenc[3] = arme[race];
            if (sexe%2 ==1)
            {
                atterenc[0] = "♂";
                atterenc[2] = malename[nom];
                ElZoria.Program.Addstat(staterenc,1,10,maxerenc);
                ElZoria.Program.Addstat(staterenc,2,10,maxerenc);
            }
            else
            {
                atterenc[0] = "♀";
                atterenc[2] = femalename[nom];
                ElZoria.Program.Addstat(staterenc,0,10,maxerenc);
                ElZoria.Program.Addstat(staterenc,4,10,maxerenc);
            }

            switch (atterenc[1])
            {
                case "Humain":
                    ElZoria.Program.Addstat(staterenc,1,10, maxerenc);
                    ElZoria.Program.Addstat(staterenc,2,15, maxerenc);
                    ElZoria.Program.Addstat(staterenc,3,5, maxerenc);
                    ElZoria.Program.Addstat(staterenc,4,10, maxerenc);
                    break;
                case "Elfe":
                    ElZoria.Program.Addstat(staterenc,1,5, maxerenc);
                    ElZoria.Program.Addstat(staterenc,2,10, maxerenc);
                    ElZoria.Program.Addstat(staterenc,3,13, maxerenc);
                    ElZoria.Program.Addstat(staterenc,4,12, maxerenc);
                    break;
                case "Nain":
                    ElZoria.Program.Addstat(staterenc,1,15, maxerenc);
                    ElZoria.Program.Addstat(staterenc,2,17, maxerenc);
                    ElZoria.Program.Addstat(staterenc,3,5, maxerenc);
                    ElZoria.Program.Addstat(staterenc,4,3, maxerenc);
                    break;
                case "Mage":
                    ElZoria.Program.Addstat(staterenc,1,8, maxerenc);
                    ElZoria.Program.Addstat(staterenc,2,14, maxerenc);
                    ElZoria.Program.Addstat(staterenc,3,15, maxerenc);
                    ElZoria.Program.Addstat(staterenc,4,8, maxerenc);
                    break;
                
            }
            StatRenc(atterenc,experenc,staterenc,maxerenc, exp[1]);
            RencAction(ref nbe, atte1,state1,maxe1,expe1,atte2,state2,maxe2,expe2,atterenc,experenc,staterenc,
                maxerenc,att,stats);
        }

        public static void RencAction(ref int nbe, string[] atte1, int[] state1, int[] maxe1, int[] expe1,
            string[] atte2, int[] state2, int[] maxe2, int[] expe2, string[] atterenc, int[] experenc,
            int[] staterenc, int[] maxerenc, string[] att, int[] stats)
        {
            Affichage.Program.InterfaceRencontre(atterenc,staterenc,maxerenc,experenc);
            ElZoria.Program.Texte();
            Console.WriteLine("Vous rencontrez quelqu'un sur votre route...");
            bool test1 = att[1] == "Elfe" && atterenc[1] == "Nain";
            bool test2 = att[1] == "Nain" && atterenc[1] == "Elfe" && atterenc[0] == "♂";
            if (test1 || test2)
            {
                Console.WriteLine("L'étranger ne t'apprécie pas à cause de ton espèce.\nIl passe son chemin.");
                ElZoria.Program.Clicktocont();
            }
            else if (atterenc[1] == "Mage" && stats[3] < 8 * experenc[1])
            {
                Console.WriteLine("L'étranger ne te trouve pas assez intelligent.\nIl passe son chemin.");
                ElZoria.Program.Clicktocont();   
            }
            else
            {
                Console.WriteLine("Veux-tu le prendre dans ton équipe ?\n 1) Oui      2) Non");
                int choix = ElZoria.Program.Ask(1, 2, 26);
                if (choix == 1)
                {
                    if (nbe == 0)
                    {
                        nbe++;
                        //fonction qui transfert à equipié 1
                        Transfert(atte1,state1,maxe1,expe1,atterenc,experenc,staterenc,maxerenc);
                    }
                    else if (nbe == 1)
                    {
                        nbe++;
                        //fonction qui transfert à equipié 1
                        Transfert(atte2,state2,maxe2,expe2,atterenc,experenc,staterenc,maxerenc);
                    }
                    else if (nbe == 2)
                    {
                        //proposer un echange
                        Console.WriteLine("Echanger: 0-Aucun  1-Equipier 1  2-Equipier 2");
                        choix = ElZoria.Program.Ask(0, 2, 28);
                        if (choix == 1)
                        {
                            //transfert à equipier 1
                            Transfert(atte1,state1,maxe1,expe1,atterenc,experenc,staterenc,maxerenc);
                        }
                        else if (choix == 2)
                        {
                            //transfert à equipier 2
                            Transfert(atte2,state2,maxe2,expe2,atterenc,experenc,staterenc,maxerenc);
                        }
                    }
                }
            }
        }

        public static void Transfert(string[] atte1, int[] state1, int[] maxe1, int[] expe1, string[] atterenc,
            int[] experenc,
            int[] staterenc, int[] maxerenc)
        {
            maxe1[0] = maxerenc[0];
            for (int i = 0; i < atte1.Length; i++)
            {
                atte1[i] = atterenc[i];
            }
            for (int i = 0; i < expe1.Length; i++)
            {
                expe1[i] = experenc[i];
            }
            for (int i = 0; i < state1.Length; i++)
            {
                state1[i] = staterenc[i];
            }
        }
        
        public static void Isitdead(ref int nbe, string[] atte1, int[] state1, int[] maxe1, int[] expe1, 
            string[] atte2, int[] state2, int[] maxe2, int[] expe2,int[] stats, int[] max, int[] bagcount, string[] bag)
        {
            string arme;
            if (nbe == 2 && state2[0]==0)
            {
                arme = atte2[3];
                Bag.Program.Add(stats,max,bagcount,bag,arme,1);
                nbe -= 1;
                maxe2[0] = 0;
                for (int i=0; i<state2.Length; i++)
                {
                    state2[i] = 0;
                }
            }

            if (state1[0] == 0)
            {
                arme = atte2[3];
                Bag.Program.Add(stats,max,bagcount,bag,arme,1);
                if (nbe == 2)
                {
                    Transfert(atte1,state1,maxe1,expe1,atte2,expe2,state2,maxe2);
                }
                else
                {
                    maxe2[0] = 0;
                    for (int i=0; i<state1.Length; i++)
                    {
                        state1[i] = 0;
                    }
                }
                nbe--;
            }
        } //ok
    }
}