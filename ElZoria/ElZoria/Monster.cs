using System;
using System.Diagnostics;
using System.Xml.Schema;

namespace Monster
{
    internal class Program
    {
        public static void main2()
        {
        }

        //int[] monstre = {vie, att, def, agil, lvl}
        public static void SelecandStat(string name, int[] statmons, int niv)
        {
            switch (name)
            {
                case "Blob": //slime
                    statmons[0] = 7 * niv;
                    statmons[1] = 7 * niv;
                    statmons[2] = 4 * niv;
                    if (20 / niv != 0)
                        statmons[3] = 20 / niv;
                    else
                        statmons[3] = 1;
                    statmons[4] = niv;
                    break;

                case "MoutMout": //mouton
                    statmons[0] = 7 * niv;
                    statmons[1] = 3 * niv;
                    statmons[2] = 2 * niv;
                    statmons[3] = niv;
                    statmons[4] = niv;
                    break;

                case "Vreli": //lapin
                    statmons[0] = 3 * niv;
                    statmons[1] = 3 * niv;
                    statmons[2] = niv;
                    statmons[3] = 10 * niv;
                    statmons[4] = niv;
                    break;

                case "Bline": //goblin
                    statmons[0] = 12 * niv;
                    statmons[1] = 12 * niv;
                    statmons[2] = 10 * niv;
                    statmons[3] = 7 * niv;
                    statmons[4] = niv;
                    break;

                case "Garou": //loup
                    statmons[0] = 14 * niv;
                    statmons[1] = 10 * niv;
                    statmons[2] = 9 * niv;
                    statmons[3] = 7 * niv;
                    statmons[4] = niv;
                    break;
            }


        } //ok

        public static void ShowMonster(string name, int niv, int x, int y)
        {
            int[] statmons = new int[5];
            x += 2;
            switch (name)
            {
                case "Blob": //slime
                    Draw.Program.Slime(x, y);
                    break;

                case "MoutMout": //mouton
                    Draw.Program.Sheep(x, y);
                    break;

                case "Vreli": //lapin
                    Draw.Program.Rabbit(x, y);
                    break;

                case "Bline": //goblin
                    Draw.Program.Goblin(x, y);
                    break;

                case "Garou": //loup
                    Draw.Program.Wolf(x, y);
                    break;

            }

            SelecandStat(name, statmons, niv);
            Console.SetCursorPosition(x + 7, y + 14);
            Console.Write("Vie : " + statmons[0]);
            Console.SetCursorPosition(x + 7, y + 15);
            Console.Write("Att : " + statmons[1]);
            Console.SetCursorPosition(x + 7, y + 16);
            Console.Write("Def : " + statmons[2]);
            Console.SetCursorPosition(x + 7, y + 17);
            Console.Write("Agi : " + statmons[3]);
        }

        public static void CombatMonstre(ref int nbe, int[] stats, string[] atte1, string[] atte2, int[] state1,
            int[] state2, int[] maxe1, int[] expe1, int[] maxe2, int[] expe2, int[] bagcount, string[] bag, int[] max,
            string mons1, int nb1, string mons2, int nb2, int niv)
        {

            int[] statmons1 = new int[5];
            int[] statmons2 = new int[5];
            SelecandStat(mons1, statmons1, niv);
            if (mons2 != "")
                SelecandStat(mons2, statmons2, niv);

            int defG = stats[1] + state1[1] + state2[1];
            int attG = stats[2] + state1[2] + state2[2];
            int agiG = stats[4] + state1[4] + state2[4];
            int coeur = 0;
            int degat1 = Comabtaux(defG, attG, agiG, statmons1);
            int degat2;
            if (mons2 != "")
                degat2 = Comabtaux(defG, attG, agiG, statmons2);
            else
                degat2 = 0;
            coeur = degat1 * nb1 + degat2 * nb2;
            int div = nbe + 1;
            stats[0] -= coeur / div;
            if (stats[0] < 0)
                stats[0] = 0;
            if (nbe > 0)
            {
                state1[0] -= coeur / div;
                if (nbe == 2)
                {
                    state2[0] -= coeur / div;
                }

                Equipe.Program.Isitdead(ref nbe, atte1, state1, maxe1, expe1, atte2, state2, maxe2, expe2,
                    stats,max,bagcount,bag);

            }
        } //ok a test quand jeu fini mais normalement ok

        public static int Comabtaux(int defG, int attG, int agiG, int[] statmons1)
        {
            int coeur = 0;
            if (agiG < statmons1[3])
            {
                coeur = 1;
            }

            if (defG < statmons1[1])
            {
                int diff = statmons1[1] - defG;
                coeur += diff;
            }

            if (attG < statmons1[2] || defG < statmons1[1])
            {
                coeur = 2 * coeur;
            }

            return coeur;
        } //ok

        //statboss vie, vie max, def, att, agi
        public static void StatBoss(string name, int[] statboss, int niv)
        {
            int add;
            if (niv == 1)
                add = 1;
            else
            {
                add = 3;
            }
            
            
            switch (name)
            {
                case "Dragnir":
                    statboss[0] = 20000;
                    statboss[1] = 20000;
                    statboss[2] = 2200;
                    statboss[3] = 2400;
                    statboss[4] = 2100;
                    break;
                case "Sorcière":
                    statboss[0] = 400 * add;
                    statboss[1] = 400 * add;
                    statboss[2] = 100 * add;
                    statboss[3] = 150 * add;
                    statboss[4] = 120 * add;
                    break;
                case "Colosse":
                    statboss[0] = 800 * add;
                    statboss[1] = 800 * add;
                    statboss[2] = 150 * add;
                    statboss[3] = 80 * add;
                    statboss[4] = 150 * add;
                    break;
                case "Arthur":
                    statboss[0] = 600 * add;
                    statboss[1] = 600 * add;
                    statboss[2] = 80 * add;
                    statboss[3] = 100 * add;
                    statboss[4] = 80 * add;
                    break;
                case "Ents":
                    statboss[0] = 400 * add;
                    statboss[1] = 400 * add;
                    statboss[2] = 120 * add;
                    statboss[3] = 70 * add;
                    statboss[4] = 150 * add;
                    break;
                case "Daimon": //boss avant fin (si envie) -> stock de 10 popo de soin +50 et 2 popo regen max
                    statboss[0] = 6000;
                    statboss[1] = 6000;
                    statboss[2] = 1000;
                    statboss[3] = 1200;
                    statboss[4] = 900;
                    break;
            }
        } //ok

        public static void ShowBoss(string name, int[] statboss, int niv, int x, int y)
        {
            x += 2;
            switch (name)
            {
                case "Dragnir":
                    Draw.Program.Dragon(x, y);
                    break;
                case "Sorcière":
                    Draw.Program.Witch(x, y);
                    break;
                case "Colosse":
                    Draw.Program.Colosse(x, y);
                    break;
                case "Arthur":
                    Draw.Program.Skeleton(x, y);
                    break;
                case "Ents":
                    Draw.Program.Ents(x, y);
                    break;
                case "Daimon":
                    Draw.Program.Demon(x, y);
                    break;
            }

            int x1 = x + 44;
            Draw.Program.Heart(x1, y);
            Console.SetCursorPosition(x1 + 5, y + 1);
            Console.Write(" Vie :");
            Console.SetCursorPosition(x1 + 5, y + 2);
            Console.Write(" " + statboss[0] + "/" + statboss[1]);
            y += 5;
            Draw.Program.Shield(x1, y);
            Console.SetCursorPosition(x1 + 5, y + 1);
            Console.Write(" Défense :");
            Console.SetCursorPosition(x1 + 5, y + 2);
            Console.Write(" " + statboss[2]);
            y += 5;
            Draw.Program.Sword(x1, y);
            Console.SetCursorPosition(x1 + 5, y + 1);
            Console.Write(" Attaque :");
            Console.SetCursorPosition(x1 + 5, y + 2);
            Console.Write(" " + statboss[3]);
            y += 5;
            Draw.Program.Boots(x1, y);
            Console.SetCursorPosition(x1 + 5, y + 1);
            Console.Write(" Agilité :");
            Console.SetCursorPosition(x1 + 5, y + 2);
            Console.Write(" " + statboss[4]);


        } //ok

        public static int CombatBoss(string[] att, int[] stats, int[] max, int[] exp, ref int nbe, string[] atte1,
            int[] state1, string[] atte2, int[] state2, int[] maxe1, int[] expe1, int[] maxe2, int[] expe2,
            int[] bagcount, string[] bag, string[] obj, int[] prix, int[] quant, int[] rp, int[] rf, int[] rmi,
            int[] rma, string name, int niv)
        {
            int[] statboss = new int[5];
            StatBoss(name, statboss, niv);
            int res = 0;
            bool combatencours = true;
            while (combatencours)
            {
                ElZoria.Program.NouvelAffichage(att, stats, max, exp, nbe, atte1, state1,
                    atte2, state2, maxe1, expe1, maxe2, expe2);
                Affichage.Program.InferfaceBoss(name, niv, statboss);
                ElZoria.Program.Texte();
                Console.WriteLine("Que veux-tu faire ?\n 1) Fuir le combat\n" +
                                  " 2) Sac\n 3) Attaquer");
                int choix = ElZoria.Program.Ask(1, 3, 27);
                int agiG = stats[4] + state1[4] + state2[4];
                int defG = stats[1] + state1[1] + state2[1];
                int attG = stats[2] + state1[2] + state2[2];
                int div = nbe + 1;
                int coeur = 0;
                switch (choix)
                {
                    case 1:
                        if (agiG < statboss[4])
                        {
                            coeur = statboss[3] - defG;
                        }

                        combatencours = false;
                        break;
                    case 2:
                        ElZoria.Program.Sacados(att, stats, max, exp, nbe, atte1, state1, atte2, state2, maxe1, expe1,
                            maxe2,
                            expe2, bagcount, bag);
                        break;
                    case 3:
                        if ((stats[7] == 2 || stats[7] == 3) && stats[0]==0)
                        {
                            stats[0] = max[0];
                            stats[7] = 0;
                        }
                        if (attG > statboss[2]) //attaque des perso 
                        {
                            coeur = attG - statboss[2];
                            statboss[0] -= coeur;
                            if (statboss[0] <= 0)
                            {
                                statboss[0] = 0;
                                res = 1;
                                combatencours = false;
                            }
                        }
                        if (defG < statboss[3] && res != 1) //attaque du dragon
                        {
                            coeur = statboss[3] - defG;
                            stats[0] -= coeur / div;
                            if (stats[0] < 0)
                            {
                                stats[0] = 0;
                                if (stats[7] == 2 || stats[7] == 3)
                                {
                                    stats[0] = max[0];
                                    stats[7] = 0;
                                }
                                else
                                {
                                    combatencours = false;   
                                }
                            }
                            if (nbe > 0)
                            {
                                state1[0] -= coeur / div;
                                if (nbe == 2)
                                {
                                    state2[0] -= coeur / div;
                                }

                                Equipe.Program.Isitdead(ref nbe, atte1, state1, maxe1, expe1, atte2, state2, maxe2,
                                    expe2, stats, max, bagcount, bag);

                            }
                        }
                        

                        break;
                }
            }
            return res;
        } //ok

    }
}