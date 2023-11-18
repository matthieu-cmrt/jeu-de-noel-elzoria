using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Schema;

namespace ElZoria
{
    internal class Program
    {
        public static void Main()
        {
            //perso princ
            string[] attributs = {"sexe", "race", "name", "arme"};
            int[] stats = {0, 0, 0, 0, 0, 0, 0, 0};
            int[] max = {0, 50}; //petit sac:50 & grand sac 150
            //Health,Defense,Attack,Intelligence,Agility,Bag,Coins
            //Grand Sac, niveau 10
            string[] races = {"Humain", "Elfe", "Nain", "Mage"};
            string[] bag = //
            {
                "Laine", "Mouton cru", "Lapin cru", "Slime", "Dent de goblin", //1
                "Mouton cuit", "Poisson cru", "Lapin cuit", "Peau de Lapin", "Slime Visqueux", //2
                "Poisson cuit", "Peau de Loup", //3
                "Poisson rare cru", //6
                "Poisson rare cuit", //8
                "Epée du début", "Arc du début", "Hache du début", "Sceptre du début", //10
                "Ecorce Ents", "Chapeau MagiK", "Roc de Golem", "Os d'Arthur", //20
                "Potion de Vie (+50)", //30
                "Canne à pêche", //40
                "Arc elfique", "Epée de chevalier", "Hache des nains", "Sceptre de sorcier", //50 
                "Potion de Vie totale", //60
                "Arc Légendaire", "Epée Légendaire", "Hache Légendaire", "Sceptre Légendaire", //100
                "Corne du démon" //1000

            };
            int[] bagcount = new int[bag.Length];
            int[] exp = {0, 1}; // xp et niveau
            //equipe
            int nbequip = 0;
            string[] attequipe1 = //Equipier 1
                {"sexe", "race", "nom", "arme"};
            string[] attequipe2 = //Equipier 2
                {"sexe", "race", "nom", "arme"};
            int[] statse1 = {0, 0, 0, 0, 0};
            int[] statse2 = {0, 0, 0, 0, 0};
            int[] expe1 = {0, 1};
            int[] expe2 = {0, 1};
            int[] maxe1 = {0};
            int[] maxe2 = {0};

            //liste shop
            string[] shobj =
            {
                "Mouton cuit", "Gros Sac", "Potion de Vie (+50)", "Potion de Vie totale", "Canne à pêche"
            };
            int[] shoprice =
            {
                10, 1000, 200, 800, 300
            };
            int[] quant =
            {
                10,1,1,1,1
            };
            //aides à implémenter 1 à 3 (Fée, Chat, Phoenix)
            //Fée (boost vie)Boss1, Chat (2e vie)Boss1, Phoenix (Boost stats +2e vie) Boss2
            
            //reste par zone
            int[] rp = {0,0,2,1}; //Blob, MoutMout, Boss, Boss secret -> 3 démon
            int[] rf = {0,0,2,1}; //Garou, Vreli, Boss, Fée
            int[] rmi = {0,2,1}; //Bline, Phoenix, Boss
            int[] rma = {0,2,1}; //Blob, peut etre fantome si pas flemme, Chat, Boss

            int[] complet = {0,0,0,0}; //dragon, stele fée, stèle chat, stèle phoenix
            
            //jeu
            Console.WriteLine("Avant de commencer, mettez la console en plein écran puis appuyez sur\nla touche " +
                              "Entrer !");
            string action = Console.ReadLine();
            Console.Clear();
            Start(attributs, races, stats, max);
            if (attributs[2] == "Ternier")
            {
                exp[1] = 999;
                exp[0] = 4500;
            } //easter egg surcheat pour mes test
            NouvelAffichage(attributs,stats,max,exp,nbequip, attequipe1, statse1, 
                attequipe2, statse2, maxe1,expe1,maxe2,expe2);
            Zesparti();
            stats[6] += 50;
            Villages(attributs,stats,max,exp,nbequip, attequipe1, statse1, attequipe2, 
                statse2, maxe1,expe1,maxe2,expe2,bagcount,bag,shobj,shoprice,quant,rp,rf,rmi,rma,complet);
            
        }

        public static void PaasageNiv(string[] att,int[] stats, int[] max, int[] exp, int add)
        {
            exp[0] += add;
            bool cont = true;
            while (cont)
            {
                if (exp[0] >= exp[1] * 10)
                {
                    exp[0] -= (10 * exp[1]);
                    PassNivStats(stats,att,max, exp[1]);
                    exp[1]++;
                }
                else
                    cont = false;
            }
        } //ok

        public static void PassNivStats(int[] stats, string[] att, int[] max, int lvl)
        {
            int nb;
            if (att[0] == "♂") //Attaque+4 def+3
            {
                Addstat(stats,1,3,max);
                Addstat(stats,2,4,max);
            }
            else //Vie+3 Agilité+2 Intelligence+2
            {
                Addstat(stats,0,3,max);
                nb = 2;
                Addstat(stats,3,nb,max);
                Addstat(stats,4,nb,max);
            }
            Addstat(stats,0,10,max);
            switch (att[1])
            {
                case "Humain":
                    Addstat(stats,1,4,max); 
                    Addstat(stats,2,5,max);
                    if (lvl%2==0) //tous les deux niv +3 en agilité
                        Addstat(stats,4,3,max);
                    if (lvl%3==0) //tous les 3 niv +2 en inté
                        Addstat(stats,3,2,max);
                    break;
                case "Elfe":
                    Addstat(stats,2,4,max); 
                    Addstat(stats,4,5,max);
                    if (lvl%2==0) //tous les deux niv +3 en inté
                        Addstat(stats,3,3,max);
                    if (lvl%3==0) //tous les 3 niv +2 en def
                        Addstat(stats,1,2,max);
                    break;
                case "Nain":
                    Addstat(stats,2,5,max); 
                    Addstat(stats,1,5,max);
                    if (lvl%2==0) //tous les deux niv +3 en agil
                        Addstat(stats,4,2,max);
                    if (lvl%3==0) //tous les 3 niv +2 en inté
                        Addstat(stats,3,2,max);
                    break;
                case "Mage":
                    Addstat(stats,2,4,max); 
                    Addstat(stats,3,4,max);
                    if (lvl%2==0) //tous les deux niv +3 en agil
                        Addstat(stats,4,3,max);
                    if (lvl%3==0) //tous les 3 niv +2 en def
                        Addstat(stats,1,3,max);
                    break;
            }
        } //ok

        public static void Texte()
        {
            Console.SetCursorPosition(0,22);
            Console.WriteLine("-----{ Elzoria }------------");
        } //ok
        public static void NouvelAffichage(string[] attributs, int[] stats, int[] max, int[] exp, int nbe, string[] atte1, 
            int[] state1, string[] atte2, int[] state2,int[] maxe1, int[] expe1,int[] maxe2, int[] expe2)
        {
            Console.Clear();
            Affichage.Program.Interfacestats(attributs,stats, max, exp);
            switch (nbe)
            {
                case 1:
                    Affichage.Program.Interfacéquipe(1, atte1, state1, maxe1, expe1);
                    break;
                case 2:
                    Affichage.Program.Interfacéquipe(1, atte1, state1, maxe1, expe1);
                    Affichage.Program.Interfacéquipe(2, atte2, state2, maxe2, expe2);
                    break;
            }
            Affichage.Program.InterfaceAide(stats[7]);
            Texte();
        } //ajout aide
        
        public static void NouvelAffichageSansAide(string[] attributs, int[] stats, int[] max, int[] exp, int nbe, string[] atte1, 
            int[] state1, string[] atte2, int[] state2,int[] maxe1, int[] expe1,int[] maxe2, int[] expe2)
        {
            Console.Clear();
            Affichage.Program.Interfacestats(attributs,stats, max, exp);
            switch (nbe)
            {
                case 1:
                    Affichage.Program.Interfacéquipe(1, atte1, state1, maxe1, expe1);
                    break;
                case 2:
                    Affichage.Program.Interfacéquipe(1, atte1, state1, maxe1, expe1);
                    Affichage.Program.Interfacéquipe(2, atte2, state2, maxe2, expe2);
                    break;
            }
            Texte();
        } //ok
        
        public static void Sacados(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1, 
            int[] state1, string[] atte2, int[] state2,int[] maxe1, int[] expe1,int[] maxe2, int[] expe2,
            int[] bagcount, string [] bag)
        {
            bool cont = true;
            while (cont)
            {
                NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                    atte2, state2, maxe1,expe1,maxe2,expe2);
                int limite = Affichage.Program.Interfacesac(max,bagcount,bag);
                Texte();
                int lim = 1;
                Console.WriteLine("Que veux-tu faire ?\n 1) Quitter le sac");
                if (stats[5] > 0)
                {
                    Console.WriteLine(" 2) Jeter un objet\n 3) Utiliser un objet");
                    lim = 3;
                }

                int action = Ask(1,lim, 27);
                if (action == 1)
                {
                    cont = false;
                }
                else if (action == 2)
                {
                    Console.WriteLine("Quel objet ?");
                    int obj = Ask(1,limite, 28);;
                    int place = Bag.Program.LocatePlace(bagcount, obj);
                    Console.WriteLine("Combien veux-tu en retirer ?");
                    int nb = Ask(0,bagcount[place], 30);
                    Bag.Program.Throw(stats,max, bagcount, bag, obj, nb);
                }
                else if (action == 3)
                {
                    Console.WriteLine("Quel objet ?");
                    int obj = Ask(1,limite, 28);
                    string arme = Bag.Program.Locate(bag, bagcount, obj);
                    int add;
                    switch (arme)
                    {
                        case "Mouton cuit":
                            Console.WriteLine("+4 de vie à                 ");
                            add = 4;
                            HealUse(stats,max,nbe, state1, state2, maxe1,maxe2,add,obj,bagcount,bag);
                            break;
                        case "Poisson cuit":
                            Console.WriteLine("+6 de vie à                 ");
                            add = 6;
                            HealUse(stats,max,nbe, state1, state2, maxe1,maxe2,add,obj,bagcount,bag);
                            break;
                        case "Lapin cuit":
                            Console.WriteLine("+4 de vie à                 ");
                            add = 4;
                            HealUse(stats,max,nbe, state1, state2, maxe1,maxe2,add,obj,bagcount,bag);
                            break;
                        case "Poisson rare cuit":
                            Console.WriteLine("+10 de vie à                ");
                            add = 10;
                            HealUse(stats,max,nbe, state1, state2, maxe1,maxe2,add,obj,bagcount,bag);
                            break;
                        
                        case "Potion de Vie (+50)":
                            Console.WriteLine("+50 de vie à                 ");
                            add = 50;
                            HealUse(stats,max,nbe, state1, state2, maxe1,maxe2,add,obj,bagcount,bag);
                            break;
                        
                        case "Potion de Vie totale":
                            add = 0;
                            HealUse(stats,max,nbe, state1, state2, maxe1,maxe2,add,obj,bagcount,bag);
                            break;
                        
                        case "Arc du début":
                        case "Epée du début":
                        case "Hache du début":
                        case "Sceptre du début":
                            WeaponUse(att, stats, max, nbe, atte1, state1, atte2, state2, maxe1,maxe2,
                                bagcount,bag,obj,0,arme);
                            break;
                        case "Arc elfique":
                        case "Epée de chevalier":
                        case "Hache des nains":
                        case "Sceptre de sorcier":
                            WeaponUse(att, stats, max, nbe, atte1, state1, atte2, state2, maxe1,maxe2,
                                bagcount,bag,obj,1,arme);
                            break;
                        case "Arc Légendaire":
                        case "Epée Légendaire":
                        case "Hache Légendaire":
                        case "Sceptre Légendaire":
                            WeaponUse(att, stats, max, nbe, atte1, state1, atte2, state2, maxe1,maxe2,
                                bagcount,bag,obj,2,arme);
                            break;
                        default:
                            Console.WriteLine("\nCet objet ne peut être utilisé.");
                            Clicktocont();
                            break;

                    }
                }
            }

        } //ok
        public static void HealUse(int[] stats, int[] max, int nbe, int[] state1, int[] state2,int[] maxe1, int[] maxe2,
            int add, int viande, int[] bagcount, string[] bag)
        { //Bag.Program.Throw(stats,max,bagcount,bag,vainde,add);
            int lim = 1;
            Console.WriteLine(" 1) Toi");
            if (nbe == 1)
            {
                lim = 2;
                Console.WriteLine(" 2) Equipier 1");
            }
            else if (nbe == 2)
            {
                lim = 3;
                Console.WriteLine(" 2) Equipier 1\n 3) Equipier 2");
            }
            int choix = Ask(1,lim, 32);

            switch (choix)
            {
                case 1:
                    if (stats[0] != max[0])
                    {
                        if (add == 0)
                            stats[0] = max[0];
                        else
                        {
                            Console.WriteLine("\nCombien veux-tu en manger ?");
                            int lim2 = Bag.Program.LocatePlace(bagcount, viande);
                            int nb = Ask(0,bagcount[lim2], 34);

                            if (nb > bagcount[lim2])
                            {
                                Console.WriteLine("Vous ne pouvez pas en manger autant !");
                                Clicktocont();
                            }
                            else
                            {
                                Bag.Program.Throw(stats,max,bagcount,bag,viande,nb);
                                if (stats[0] + nb*add >= max[0])
                                    stats[0] = max[0];
                                else
                                    stats[0] += nb*add;   
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tu as déjà toute ta vie.");
                        Clicktocont();
                    }
                    break;
                case 2:
                    if (state1[0] != maxe1[0])
                    {
                        if (add == 0)
                            state1[0] = maxe1[0];
                        else
                        {
                            Console.WriteLine("\nCombien veux-tu en manger ?");
                            int lim2 = Bag.Program.LocatePlace(bagcount, viande);
                            int nb = Ask(0,bagcount[lim2], 34);

                            if (nb > bagcount[lim2])
                            {
                                Console.WriteLine("Vous ne pouvez pas en manger autant !");
                                Clicktocont();
                            }
                            else
                            {
                                Bag.Program.Throw(stats,max,bagcount,bag,viande,nb);
                                if (state1[0]+ nb*add >= maxe1[0])
                                    state1[0] = maxe1[0];
                                else
                                    state1[0] += nb*add;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ton équipier as déjà toute sa vie.");
                        Clicktocont();
                    }
                    break;
                case 3:
                    if (state2[0] != maxe2[0])
                    {
                        if (add == 0)
                            state2[0] = maxe2[0];
                        else
                        {
                            Console.WriteLine("\nCombien veux-tu en manger ?");
                            int lim2 = Bag.Program.LocatePlace(bagcount, viande);
                            int nb = Ask(0,bagcount[lim2], 34);

                            if (nb > bagcount[lim2])
                            {
                                Console.WriteLine("Vous ne pouvez pas en manger autant !");
                                Clicktocont();
                            }
                            else
                            {
                                Bag.Program.Throw(stats,max,bagcount,bag,viande,nb);
                                if (state2[0] + nb*add >= maxe2[0])
                                    state2[0] = maxe2[0];
                                else
                                    state2[0] += nb*add;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ton équipier as déjà toute sa vie.");
                        Clicktocont();
                    }
                    break;
            }
        } //ok

        public static void WeaponUse(string[] att, int[] stats, int[] max, int nbe, string[] atte1,
            int[] state1, string[] atte2, int[] state2, int[] maxe1, int[] maxe2,
            int[] bagcount, string[] bag, int obj, int classweap, string arme)
        {
            int lim = 1;
            Console.WriteLine("A qui veux-tu donner l'arme ?\n 1) Toi");
            if (nbe == 1)
            {
                lim = 2;
                Console.WriteLine(" 2) Equipier 1");
            }
            else if (nbe==2)
            {
                lim = 3;
                Console.WriteLine(" 2) Equipier 1\n 3) Equipier 2");
            }
            int choix = Ask(1,lim, 32);
            bool test1;
            bool test2;
            int v;
            switch (choix)
            {
                case 1:
                    test1 = classweap == 1 && stats[3] < 40;
                    test2 = classweap == 2 && stats[3] < 90;
                    v = 0;
                    if (classweap == 1)
                        v = 40;
                    else if (classweap == 2)
                        v = 90;
                    if (test1 || test2)
                    {
                        Console.WriteLine("Tu n'as pas assez d'intelligence pour utilisé cette arme.\n" +
                                          "Il t'en faut au moins {0}",v);
                        Clicktocont();
                    }
                    else
                    {
                        weaponrace(att,max,stats,bagcount,bag,arme,obj);   
                    }
                    break;
                case 2:
                    test1 = classweap == 1 && state1[3] < 40;
                    test2 = classweap == 2 && state1[3] < 90;
                    v = 0;
                    if (classweap == 1)
                        v = 40;
                    else if (classweap == 2)
                        v = 90;
                    if (test1 || test2)
                    {
                        Console.WriteLine("Ton équipier n'as pas assez d'intelligence pour utilisé cette\narme. " +
                                          "Il lui en faut au moins {0}",v);
                        Clicktocont();
                    }
                    else
                        weaponrace(atte1,maxe1,state1,bagcount,bag,arme,obj);
                    break;
                case 3:
                    test1 = classweap == 1 && state2[3] < 40;
                    test2 = classweap == 2 && state2[3] < 90;
                    v = 0;
                    if (classweap == 1)
                        v = 40;
                    else if (classweap == 2)
                        v = 90;
                    if (test1 || test2)
                    {
                        Console.WriteLine("Ton équipier n'as pas assez d'intelligence pour utilisé cette\narme. " +
                                          "Il lui en faut au moins {0}",v);
                        Clicktocont();
                    }
                    else
                        weaponrace(atte2,maxe2,state2,bagcount,bag,arme,obj);
                    break;
            }
        } //ok

        public static void weaponrace(string[] att, int[] max, int[] stats, int[] bagcount, string[] bag, 
            string arme, int obj)
        {
            switch (att[1])
            {
                case "Humain": 
                    if (arme == "Epée du début" || arme == "Epée de chevalier" || arme == "Epée Légendaire")
                    {
                        StatWeapon(stats, max,att[3], arme);
                        Bag.Program.Swap(stats,max,bagcount,bag,obj,att[3],1);
                        //Bag.Program.Throw(stats, max, bagcount, bag, obj, 1);
                        //Bag.Program.Add2(stats, max, bagcount, bag, att[3], 1);
                        att[3] = arme;
                    }
                    else
                    {
                        Console.WriteLine("L'espèce de ce personnage et l'arme ne sont pas\ncompatibles.");
                        Clicktocont();
                    }
                    break;
                case "Elfe":
                    if (arme == "Arc du début" || arme == "Arc elfique" || arme == "Arc Légendaire")
                    {
                        StatWeapon(stats,max, att[3], arme);
                        Bag.Program.Swap(stats,max,bagcount,bag,obj,att[3],1);
                        att[3] = arme;
                    }
                    else
                    {
                        Console.WriteLine("L'espèce de ce personnage et l'arme ne sont pas\ncompatibles.");
                        Clicktocont();
                    }
                    break;
                case "Nain":
                    if (arme == "Hache du début" || arme == "Hache des nains" || arme == "Hache Légendaire")
                    {
                        StatWeapon(stats,max, att[3], arme);
                        Bag.Program.Swap(stats,max,bagcount,bag,obj,att[3],1);
                        att[3] = arme;
                    }
                    else
                    {
                        Console.WriteLine("L'espèce de ce personnage et l'arme ne sont pas\ncompatibles.");
                        Clicktocont();
                    }
                    break;
                case "Mage":
                    if (arme == "Sceptre du début" || arme == "Sceptre de sorcier" || arme == "Sceptre Légendaire")
                    {
                        StatWeapon(stats,max, att[3], arme);
                        Bag.Program.Swap(stats,max,bagcount,bag,obj,att[3],1);
                        att[3] = arme;
                        Clicktocont();
                    }
                    else
                    {
                        Console.WriteLine("L'espèce de ce personnage et l'arme ne sont pas\ncompatibles.");
                        Clicktocont();
                    }
                    break;
            }
        } //ok

        public static void StatWeapon(int[] stats, int[] max, string oldweapon, string newweapon)
        {
            int nb;
            switch (oldweapon)
            {
                //Epées
                case "Epée du début":
                    if (newweapon == "Epée Légendaire")
                    {
                        nb = 20;
                        Addstat(stats,0,nb,max);
                        nb = 49;
                        Addstat(stats,1,nb,max);
                        nb = 95;
                        Addstat(stats,2,nb,max);
                        nb = 25;
                        Addstat(stats,3,nb,max);
                        Addstat(stats,4,nb,max);
                    }
                    else
                    {
                        nb = 24;
                        Addstat(stats,1,nb,max);
                        nb = 45;
                        Addstat(stats,2,nb,max);
                        nb = 12;
                        Addstat(stats,4,nb,max);
                    }
                    break;
                case "Epée de chevalier":
                    if (newweapon == "Epée Légendaire")
                    {
                        nb = 20;
                        Addstat(stats,0,nb,max);
                        nb = 25;
                        Addstat(stats,1,nb,max);
                        nb = 50;
                        Addstat(stats,2,nb,max);
                        nb = 13;
                        Addstat(stats,4,nb,max);
                        nb = 25;
                        Addstat(stats,3,nb,max);
                    }
                    else
                    {
                        nb = -24;
                        Addstat(stats,1,nb,max);
                        nb = -45;
                        Addstat(stats,2,nb,max);
                        nb = -12;
                        Addstat(stats,4,nb,max);
                    }
                    break;
                case "Epée Légendaire":
                    if (newweapon == "Epée de chevalier")
                    {
                        nb = -20;
                        Addstat(stats,0,nb,max);
                        nb = -25;
                        Addstat(stats,1,nb,max);
                        nb = -50;
                        Addstat(stats,2,nb,max);
                        nb = -13;
                        Addstat(stats,4,nb,max);
                        nb = -25;
                        Addstat(stats,3,nb,max);
                    }
                    else
                    {
                        nb = -20;
                        Addstat(stats,0,nb,max);
                        nb = -49;
                        Addstat(stats,1,nb,max);
                        nb = -95;
                        Addstat(stats,2,nb,max);
                        nb = -25;
                        Addstat(stats,3,nb,max);
                        Addstat(stats,4,nb,max);
                    }
                    break;
                
                //Arcs
                case "Arc du début":
                    if (newweapon == "Arc Légendaire")
                    {
                        nb = 20;
                        Addstat(stats,0,nb,max);
                        nb = 20;
                        Addstat(stats,1,nb,max);
                        nb = 72;
                        Addstat(stats,2,nb,max);
                        Addstat(stats,4,nb,max);
                        nb = 30;
                        Addstat(stats,3,nb,max);
                    }
                    else
                    {
                        nb = 5;
                        Addstat(stats,1,nb,max);
                        nb = 32;
                        Addstat(stats,2,nb,max);
                        Addstat(stats,4,nb,max);
                        nb = 15;
                        Addstat(stats,3,nb,max);
                    }
                    break;
                case "Arc elfique":
                    if (newweapon == "Arc Légendaire")
                    {
                        nb = 20;
                        Addstat(stats,0,nb,max);
                        nb = 5;
                        Addstat(stats,1,nb,max);
                        nb = 40;
                        Addstat(stats,2,nb,max);
                        Addstat(stats,4,nb,max);
                        nb = 18;
                        Addstat(stats,3,nb,max);
                    }
                    else
                    {
                        nb = -5;
                        Addstat(stats,1,nb,max);
                        nb = -32;
                        Addstat(stats,2,nb,max);
                        Addstat(stats,4,nb,max);
                        nb = -15;
                        Addstat(stats,3,nb,max);
                    }
                    break;
                case "Arc Légendaire":
                    if (newweapon == "Arc elfique")
                    {
                        nb = -20;
                        Addstat(stats,0,nb,max);
                        nb = -5;
                        Addstat(stats,1,nb,max);
                        nb = -40;
                        Addstat(stats,2,nb,max);
                        Addstat(stats,4,nb,max);
                        nb = -18;
                        Addstat(stats,3,nb,max);
                    }
                    else
                    {
                        nb = -20;
                        Addstat(stats,0,nb,max);
                        nb = -20;
                        Addstat(stats,1,nb,max);
                        nb = -72;
                        Addstat(stats,2,nb,max);
                        Addstat(stats,4,nb,max);
                        nb = -30;
                        Addstat(stats,3,nb,max);
                    }
                    break;
                
                //Haches
                case "Hache du début":
                    if (newweapon == "Hache Légendaire")
                    {
                        nb = 20;
                        Addstat(stats,0,nb,max);
                        nb = 88;
                        Addstat(stats,1,nb,max);
                        Addstat(stats,2,nb,max);
                        nb = 10;
                        Addstat(stats,4,nb,max);
                        nb = 8;
                        Addstat(stats,3,nb,max);
                    }
                    else
                    {
                        nb = 33;
                        Addstat(stats,1,nb,max);
                        nb = 36;
                        Addstat(stats,2,nb,max);
                        nb = 6;
                        Addstat(stats,4,nb,max);
                        Addstat(stats,3,nb,max);
                    }
                    break;
                case "Hache des nains":
                    if (newweapon == "Hache Légendaire")
                    {
                        nb = 20;
                        Addstat(stats,0,nb,max);
                        nb = 55;
                        Addstat(stats,1,nb,max);
                        nb = 52;
                        Addstat(stats,2,nb,max);
                        nb = 4;
                        Addstat(stats,4,nb,max);
                        nb = 2;
                        Addstat(stats,3,nb,max);
                    }
                    else
                    {
                        nb = -33;
                        Addstat(stats,1,nb,max);
                        nb = -36;
                        Addstat(stats,2,nb,max);
                        nb = -6;
                        Addstat(stats,3,nb,max);
                        Addstat(stats,4,nb,max);
                    }
                    break;
                case "Hache Légendaire":
                    if (newweapon == "Hache des nains")
                    {
                        nb = -20;
                        Addstat(stats,0,nb,max);
                        nb = -55;
                        Addstat(stats,1,nb,max);
                        nb = -52;
                        Addstat(stats,2,nb,max);
                        nb = -4;
                        Addstat(stats,4,nb,max);
                        nb = -2;
                        Addstat(stats,3,nb,max);
                    }
                    else
                    {
                        nb = -20;
                        Addstat(stats,0,nb,max);
                        nb = -88;
                        Addstat(stats,1,nb,max);
                        Addstat(stats,2,nb,max);
                        nb = -10;
                        Addstat(stats,4,nb,max);
                        nb = -8;
                        Addstat(stats,3,nb,max);  
                    }
                    break;
                
                //Sceptres
                case "Sceptre du début":
                    if (newweapon == "Sceptre Légendaire")
                    {
                        nb = 20;
                        Addstat(stats,0,nb,max);
                        nb = 30;
                        Addstat(stats,1,nb,max);
                        nb = 68;
                        Addstat(stats,2,nb,max);
                        Addstat(stats,4,nb,max);
                        nb = 48;
                        Addstat(stats,3,nb,max);
                    }
                    else
                    {
                        nb = 7;
                        Addstat(stats,1,nb,max);
                        nb = 28;
                        Addstat(stats,2,nb,max);
                        nb = 23;
                        Addstat(stats,4,nb,max);
                        Addstat(stats,3,nb,max);
                    }
                    break;
                case "Sceptre de sorcier":
                    if (newweapon == "Sceptre Légendaire")
                    {
                        nb = 20;
                        Addstat(stats,0,nb,max);
                        nb = 23;
                        Addstat(stats,1,nb,max);
                        nb = 40;
                        Addstat(stats,2,nb,max);
                        nb = 45;
                        Addstat(stats,4,nb,max);
                        nb = 25;
                        Addstat(stats,3,nb,max);
                    }
                    else
                    {
                        nb = -7;
                        Addstat(stats,1,nb,max);
                        nb = -28;
                        Addstat(stats,2,nb,max);
                        nb = -23;
                        Addstat(stats,4,nb,max);
                        Addstat(stats,3,nb,max);
                    }
                    break;
                case "Sceptre Légendaire":
                    if (newweapon == "Sceptre de sorcier")
                    {
                        nb = -20;
                        Addstat(stats,0,nb,max);
                        nb = -23;
                        Addstat(stats,1,nb,max);
                        nb = -40;
                        Addstat(stats,2,nb,max);
                        nb = -45;
                        Addstat(stats,4,nb,max);
                        nb = -25;
                        Addstat(stats,3,nb,max);
                    }
                    else
                    {
                        nb = -20;
                        Addstat(stats,0,nb,max);
                        nb = -30;
                        Addstat(stats,1,nb,max);
                        nb = -68;
                        Addstat(stats,2,nb,max);
                        Addstat(stats,4,nb,max);
                        nb = -48;
                        Addstat(stats,3,nb,max);   
                    }
                    break;
            }
        } //ok
        
        public static void Clicktocont()
        {
            Console.WriteLine("Appuies sur Entrer pour continuer"); 
            Console.ReadLine();
        } //ok
        public static void Shop(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1,
            int[] state1, string[] atte2, int[] state2, int[] maxe1, int[] expe1, int[] maxe2, int[] expe2,
            int[] bagcount, string[] bag, string[] obj, int[] prix, int[] quant)
        {
            bool cont = true;
            while (cont)
            {
                NouvelAffichageSansAide(att,stats,max,exp,nbe, atte1, state1, 
                    atte2, state2, maxe1,expe1,maxe2,expe2);
                int limite = Affichage.Program.Interfacesac(max,bagcount,bag);
                Affichage.Program.Interfaceshop(prix, obj, quant);
                Texte();
                int lim = 2;
                Console.WriteLine("Bienvenue au Magasin ! Seul le mouton cuit y est\ntoujours présent ! " +
                                  "Le reste est approvisionné\naprès chaque Boss!");
                Console.WriteLine("Que veux-tu faire ?\n 1) Quitter le magasin\n 2) Acheter un objet");
                if (stats[5] > 0)
                {
                    Console.WriteLine(" 3) Vendre un objet");
                    lim = 3;
                }
                int a = Ask(1,lim, 30);
                if (a == 1)
                    cont = false;
                else if (a == 2)
                {
                    int nb;
                    Console.WriteLine("Que veux-tu acheter ?                  ");
                    int achat = Ask(1,5, 32);
                    Console.WriteLine("Combien veux-tu en acheter ?            ");
                    if (achat == 1)
                    {
                        nb=Ask(0,10, 34);
                        if (nb * 10 <= stats[6])
                        {
                            if (nb <= max[1] - stats[5] || quant[achat-1] != 0)
                            {
                                stats[6] -= nb * 10;
                                quant[0] -= nb;
                                Bag.Program.Add(stats, max, bagcount,bag,"Mouton cuit", nb);
                            }
                            else
                            {
                                Console.WriteLine("Vous n'avez pas assez de place ou l'objet est\ndéjà vendu !");
                                Clicktocont();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vous n'avez pas assez d'argent !");
                            Clicktocont();
                        }
                    }
                    else if (achat == 2)
                    {
                        if (1000 <= stats[6])
                        {
                            if (quant[1] != 0)
                            {
                                stats[6] -= 1000;
                                quant[1] -= 1;
                                max[1] += 100;
                            }
                            else
                            {
                                Console.WriteLine("L'objet est déjà vendu !");
                                Clicktocont();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vous n'avez pas assez d'argent !");
                            Clicktocont();
                        }
                    }
                    else
                    {
                        nb = Ask(0,quant[achat-1], 32);
                        if (nb * prix[achat-1] <= stats[6])
                        {
                            if (nb <= max[1] - stats[5] || quant[achat-1] != 0)
                            {
                                stats[6] -= nb * prix[achat-1];
                                quant[achat-1] -= nb;
                                Bag.Program.Add(stats, max, bagcount,bag,obj[achat-1], nb);
                            }
                            else
                            {
                                Console.WriteLine("Vous n'avez pas assez de place ou l'objet est\ndéjà vendu !");
                                Clicktocont();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vous n'avez pas assez d'argent !");
                            Clicktocont();
                        }
                    }
                        
                }
                else if (a == 3)
                {
                    bool cont2 = true;
                    while (cont2)
                    {
                        Console.SetCursorPosition(0,31);
                        Console.WriteLine("Que veux-tu vendre ?");
                        int vente = Ask(1,limite, 32);
                        Console.WriteLine("\nCombien veux-tu en vendre ?");
                        int lim2 = Bag.Program.LocatePlace(bagcount, vente);
                        int nb = Ask(0,bagcount[lim2], 34);

                        if (nb > bagcount[lim2])
                        {
                            Console.WriteLine("Vous ne pouvez pas en vendre autant !");
                        }
                        else
                        {
                            cont2 = false;
                            int price = PrixVente(vente, bag, bagcount);
                            //bagcount[lim2] -= nb;
                            stats[6] += nb * price;
                            Bag.Program.Throw(stats,max,bagcount,bag,vente,nb);
                        }
                    }
                }
            }
        } //ok
        public static int PrixVente(int obj,string[] bag, int[] bagcount)
        {
            string objet = Bag.Program.Locate(bag, bagcount, obj);
            switch (objet)
            {
                case "Laine":
                case "Mouton cru":
                case "Lapin cru":
                case "Slime":
                case "Dent de goblin":
                    return 1;
                case "Poisson cru":
                case "Slime Visqueux":
                case "Mouton cuit":
                case "Lapin cuit":
                case "Peau de Lapin":
                    return 2;
                case "Poisson cuit":
                case "Peau de Loup":
                    return 3;
                case "Poisson rare cru":
                    return 6;
                case "Poisson rare cuit":
                    return 8;
                case "Arc du début":
                case "Epée du début": 
                case "Hache du début":
                case "Sceptre du début":
                    return 10;
                case "Ecorce Ents":
                case "Chapeau MagiK":
                case "Roc de Golem":
                case "Os d'Arthur":
                    return 20;
                case "Potion de Vie (+50)":
                    return 30;
                case "Canne à pêche":
                    return 40;
                case "Arc elfique":
                case "Epée de chevalier": 
                case "Hache des nains":
                case "Sceptre de sorcier":
                    return 50;
                case "Potion de Vie totale":
                    return 60;
                case "Arc Légendaire":
                case "Epée Légendaire":
                case "Hache Légendaire": 
                case "Sceptre Légendaire":
                    return 100;
                case "Corne du démon":
                    return 1000;
                default:
                    return 0;
            }
        } //ok
        
        public static int Ask(int depart,int lim, int y)
        {
            int nb = 0;
            bool verif = true;
            while (verif)
            {
                string number = Console.ReadLine();
                Console.SetCursorPosition(0, y);
                if (number != "")
                {
                    for (int i = 0; i < number.Length; i++)
                    {
                        if (number[i] < '0' || number[i] > '9')
                            verif = false;
                    }

                    
                    if (verif)
                    {
                        nb = Convert.ToInt32(number);
                        if (nb > lim || nb < depart)
                        {
                            Console.WriteLine("Tu dois entrer un nombre entre "+depart+" et " + lim + " !");
                        }
                        else
                        {
                            verif = false;
                        }
                    }
                    else
                    {
                        verif = true;
                        Console.WriteLine("Tu dois entrer un nombre entre 1 et " + lim + " !");
                    }
                }
                else
                {
                    Console.WriteLine("Tu dois entrer un nombre entre 1 et " + lim + " !");
                }


            }

            return nb;

        } //ok

        public static void Addstat(int[] stats, int stat, int nb, int[] max)
        {
            stats[stat] += nb;
            if (stat == 0)
                max[0] += nb;
        } //ok

        public static void Start(string[] attributs, string[] races, int[] stats, int[] max)
        {
            Console.WriteLine("-----{ Elzoria }------------");
            Console.WriteLine("Bonjour jeune aventurier, tu t'apprêtes à rejoindre mon monde, celui de ElZoria ! Je t'ai convoqué");
            Console.WriteLine("en ce jour pour te demander ton aide. Mon monde court à sa perte et toi héro des temps anciens, est");
            Console.WriteLine("le seul à pouvoir m'aider. Attention chacun de tes choix aura une influence sur ton aventure !");
            Console.WriteLine("Avant de commencer, es-tu un mâle (1) ou une femelle (2) ? (Ecris un nombre)");
            string sexe = "Neutral";

            int s = Ask(1,2,5);
                if (s == 1)
                {
                    sexe = "♂";
                    Addstat(stats,1,10,max);
                    Addstat(stats,2,10,max);
                }
                else if (s == 2)
                {
                    sexe = "♀";
                    Addstat(stats,0,10,max);
                    Addstat(stats,4,10,max);
                }
                
            attributs[0] = sexe;
            Console.Clear();
            if (sexe == "♂")
            {
                Console.WriteLine("Tu es donc un mâle.");
            }
            else
            {
                Console.WriteLine("Tu es donc une femelle.");
            }
            Console.WriteLine("Mais de quelle tribu viens-tu ?");
            int r2 = 1;
            Affichage.Program.PrintDebut(sexe);
                Console.WriteLine();
                int r = Ask(1,4, 2);
                Addstat(stats,0,40,max);
                if (r == 1)
                {
                    r2 = r-1;
                    attributs[1] = races[r2];
                    Addstat(stats,1,10, max);
                    Addstat(stats,2,15, max);
                    Addstat(stats,3,5, max);
                    Addstat(stats, 4, 10, max);
                    attributs[3] = "Epée du début";
                }
                else if (r == 2)
                {
                    r2 = r-1;
                    attributs[1] = races[r2];
                    Addstat(stats,1,5, max);
                    Addstat(stats,2,10, max);
                    Addstat(stats,3,13, max);
                    Addstat(stats, 4, 12, max);
                    attributs[3] = "Arc du début";
                }
                else if (r == 3)
                {
                    r2 = r - 1;
                    attributs[1] = races[r2];
                    Addstat(stats,1,15, max);
                    Addstat(stats,2,17, max);
                    Addstat(stats,3,5, max);
                    Addstat(stats, 4, 3, max);
                    attributs[3] = "Hache du début";
                }
                else if (r == 4)
                {
                    r2 = r-1;
                    attributs[1] = races[r2];
                    Addstat(stats,1,8, max);
                    Addstat(stats,2,14, max);
                    Addstat(stats,3,15, max);
                    Addstat(stats, 4, 8, max);
                    attributs[3] = "Sceptre du début";
                }
                
                Console.WriteLine("Tu as décidé d'être un " + races[r2] + ".");
            Console.Clear();
            Console.WriteLine("Quel est ton nom ?");

            bool erreur = true;
            while (erreur)
            {
                string name = Console.ReadLine();

                if (name.Length <= 11)
                {
                    attributs[2] = name;
                    erreur = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ton nom doit contenir moins de 11 caractères ! Quel est ton nom ?");
                }

                if (name == "Ternier")
                {
                    stats[0] = 9999;
                    stats[1] = 9999;
                    stats[2] = 9999;
                    stats[3] = 9999;
                    stats[4] = 9999;
                    stats[6] = 99949;
                    max[0] = 9999;
                    max[1] = 150;
                }
            }
        } //ok

        public static void Zesparti() 
        {
            Console.WriteLine("Enfin ! Jeune héro des temps anciens, tu vas enfin pouvoir te lancer" +
                              "\ndans ta quête. Pour preuve de ma gratitude envers toi, voici 50 coins" +
                              "\nafin de pouvoir bien commencer. Rappelle toi ! Si tu meurs ici, tu" +
                              "\nmeurs dans ton monde ! En gros si tu meurs c'est fini, tu devras\n" +
                              "recommencer ! Bonne chance à toi jeune Héro ! ");
            Clicktocont();
        } //ok
        
        public static void Villages(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1, 
            int[] state1, string[] atte2, int[] state2,int[] maxe1, int[] expe1,int[] maxe2, int[] expe2,
            int[] bagcount, string [] bag,string[] obj, int[] prix, int[] quant, int[] rp,int[] rf,
            int[] rmi,int[] rma, int[] complet)
        {
            NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                atte2, state2, maxe1,expe1,maxe2,expe2);
            Console.Write("Bienvenue dans au village de départ !\n");
            Console.Write("Que veux-tu faire ?\n 1) Sac\n 2) Magasin \n 3) Sortir du village\n");
            int action = Ask(1,3, 28);
                switch (action)
                {
                    case 1: //sac
                        Sacados(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag);
                        Villages(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix, quant,rp,rf,rmi,rma,complet);
                        break;
                    
                    case 2: //magasin
                        quant[0] = 10;
                        Shop(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant);
                        Villages(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                        break;
                    case 3: //partir
                        TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                        break;
                    
                }
        } //ok

        public static void TerresElZoria(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1, 
            int[] state1, string[] atte2, int[] state2,int[] maxe1, int[] expe1,int[] maxe2, int[] expe2,
            int[] bagcount, string [] bag,string[] obj, int[] prix, int[] quant, int[] rp,int[] rf,
            int[] rmi,int[] rma, int[] complet)
        {
            NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                atte2, state2, maxe1,expe1,maxe2,expe2);
            Console.Write("Bienvenue dans les terres d'ElZoria !\n");
            Console.WriteLine("Où veux-tu aller ?\n 1) La Plaine\n 2) La Forêt\n 3) Les Marécages\n 4) La Mine\n 5) La Grotte du Dragon\n" +
                              " 6) Le lac\n 7) Le village");
            int choice = Ask(1,7, 32);
            int lvl = 0;
            NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                atte2, state2, maxe1,expe1,maxe2,expe2);
            Equipe.Program.RencontreProba(ref nbe, atte1, state1,maxe1,expe1,atte2, state2,maxe2,expe2,exp,att,stats);
            switch (choice)
            {
                case 1:
                    Plaine(att,stats,max,exp,nbe, atte1, state1, atte2, state2, maxe1,expe1,
                        maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                    break;
                case 2:
                    Foret(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                    break;
                case 3:
                    Marécages(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                    break;
                case 4:
                    Mine(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                    break;
                case 5:
                    GrotteDrag(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                    break;
                case 6:
                    Lac(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                    break;
                case 7:
                    Villages(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                    break;
            }
        }

        public static void Lac(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1,
            int[] state1, string[] atte2, int[] state2, int[] maxe1, int[] expe1, int[] maxe2, int[] expe2,
            int[] bagcount, string[] bag, string[] obj, int[] prix, int[] quant, int[] rp,
            int[] rf,int[] rmi,int[] rma, int[] complet)
        {
            NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                atte2, state2, maxe1,expe1,maxe2,expe2);
            Console.WriteLine("Bienvenue au Lac !");
            if (bagcount[23] == 0)
                Console.WriteLine("J'aurais bien aimé avoir une canne à pêche pour\nattraper un deux poisson !");
            Console.WriteLine("Que veux-tu faire ?\n 1) Partir\n 2) Sac");
            int nb = 3;
            int lim = 2;
            bool testcuire = false;
            if (bagcount[1] != 0 || bagcount[2] != 0 || bagcount[6] != 0 || bagcount[11] != 0)
            {
                Console.WriteLine(" "+nb+") Cuire de la viande");
                nb++;
                lim++;
                testcuire = true;
            }
            if (bagcount[23] != 0)
            {
                Console.WriteLine(" "+nb+") Pêcher");
                lim++;
            }

            int choix = Ask(1,lim, 33);
            string poiscaille;
            int add;
            switch (choix)
            {
                case 1:
                    TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                    break;
                case 2:
                    Sacados(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag);
                    Lac(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix, quant,rp,rf,rmi,rma,complet);
                    break;
                case 3:
                    if (testcuire)
                    {
                        Cuisson(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag);
                    }
                    else
                    {
                        if (stats[5]!=max[1])
                        {
                            poiscaille = Peche();
                            Console.WriteLine("Tu as eu un "+poiscaille+".");
                            Bag.Program.Add(stats,max,bagcount,bag,poiscaille,1);
                            if (poiscaille == "Poisson cru")
                                add = 2;
                            else
                                add = 4;
                            PaasageNiv(att,stats,max,exp,add);
                        }
                        else
                        {
                            Console.WriteLine("Tu n'as plus de place dans ton sac !");
                        }
                        Clicktocont();
                    }
                    Lac(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix, quant,rp,rf,rmi,rma,complet);
                    break;
                case 4:
                    if (stats[5]!=max[1])
                    {
                        poiscaille = Peche();
                        Console.WriteLine("Tu as eu un "+poiscaille+".");
                        Bag.Program.Add(stats,max,bagcount,bag,poiscaille,1);
                        if (poiscaille == "Poisson cru")
                            add = 2;
                        else
                            add = 4;
                        PaasageNiv(att,stats,max,exp,add);
                    }
                    else
                    {
                        Console.WriteLine("Tu n'as plus de place dans ton sac !");
                    }
                    Clicktocont();
                    Lac(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix, quant,rp,rf,rmi,rma,complet);
                    break;
            }
        } //ok
        public static string Peche()
        {
            Random rnd= new Random();
            int proba = rnd.Next(1, 15);
            switch (proba)
            {
                case 1:
                    return "Poisson rare cru";
                default:
                    return "Poisson cru";
            }
        } //ok

        public static void Cuisson(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1,
            int[] state1, string[] atte2, int[] state2, int[] maxe1, int[] expe1, int[] maxe2, int[] expe2,
            int[] bagcount, string[] bag)
        {
            bool cont = true;
            while (cont)
            {
                NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                    atte2, state2, maxe1,expe1,maxe2,expe2);
                int limite = Affichage.Program.Interfacesac(max, bagcount, bag);
                Texte();
                Console.WriteLine("Que veux-tu faire ?\n 1) Quitter le feu\n 2) Cuire une viande");
                int choix = Ask(1,2, 26);
                if (choix == 1)
                    cont = false;
                else
                {
                    Console.WriteLine("Quel viande veux-tu cuire ?");
                    int viande = Ask(1,limite, 28);
                    int nb;
                    string v = Bag.Program.Locate(bag, bagcount, viande);
                    switch (v)
                    {
                        case "Mouton cru":
                            nb = CombienViande(viande, bagcount);
                            Bag.Program.Swap(stats,max,bagcount,bag, viande, "Mouton cuit",nb);
                            Console.WriteLine("Tu as cuit "+nb+" "+v);
                            Clicktocont();
                            break;
                        case "Lapin cru":
                            nb = CombienViande(viande, bagcount);
                            Bag.Program.Swap(stats,max,bagcount,bag, viande, "Lapin cuit",nb);
                            Console.WriteLine("Tu as cuit "+nb+" "+v);
                            Clicktocont();
                            break;
                        case "Poisson cru":
                            nb = CombienViande(viande, bagcount);
                            Bag.Program.Swap(stats,max,bagcount,bag, viande, "Poisson cuit",nb);
                            Console.WriteLine("Tu as cuit "+nb+" "+v);
                            Clicktocont();
                            break;
                            
                        case "Poisson rare cru":
                            nb = CombienViande(viande, bagcount);
                            Bag.Program.Swap(stats,max,bagcount,bag, viande, "Poisson rare cuit",nb);
                            Console.WriteLine("Tu as cuit "+nb+" "+v);
                            Clicktocont();
                            break;
                            
                        default:
                            Console.WriteLine("Cet objet ne peut pas être cuit !");
                            Clicktocont();
                            break;
                    }
                }
            }
        } //ok
        
        public static int CombienViande(int indice, int[] bagcount)
        {
            Console.WriteLine("Combien veux-tu en cuire ?");
            int place = Bag.Program.LocatePlace(bagcount, indice);
            int lim = bagcount[place];
            int nb = Ask(0,lim,30);
            return nb;
        } //ok

        public static void Plaine(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1,
            int[] state1, string[] atte2, int[] state2, int[] maxe1, int[] expe1, int[] maxe2, int[] expe2,
            int[] bagcount, string[] bag, string[] obj, int[] prix, int[] quant, int lvl, int[] rp,
            int[] rf,int[] rmi,int[] rma, int[] complet)
        {
            NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                atte2, state2, maxe1,expe1,maxe2,expe2);
            int y = 0;
            Random rnd= new Random();
            Console.WriteLine("Bienvenue à la Plaine de niveau {0} !", lvl);
            bool test1 = lvl == 5 && rp[2] == 2;
            bool test2 = lvl == 10 && rp[2] == 1;
            bool test3 = lvl == 15 && rp[3] != 0;
            bool test = test1 || test2 || test3; //|| !test
            int niv = 0;
            string name = "";
            if (lvl != 0 && !test)
            {
                if (rp[0] == 0 && rp[0] == 0)
                {
                    int nb1 = rnd.Next(1, lvl);
                    int nb2 = rnd.Next(0, lvl);
                    rp[0] = nb1;
                    rp[1] = nb2;
                }
                Console.WriteLine("Il y a {0} Blob et {1} MoutMout", rp[0],rp[1]);
                Affichage.Program.InterfaceMonstre("Blob",lvl,0);
                Affichage.Program.InterfaceMonstre("MoutMout",lvl,1);
                Console.SetCursorPosition(0,25);
            }
            else if (test1)
            {
                niv = 1;
                Console.WriteLine("Tu es arrivé à la tour de boss de niveau {0} !",niv);
                Console.WriteLine("Attention il faut que tu aies de l'espace libre\n" +
                                  "dans ton sac pour avoir les objets du boss !");
                name = "Arthur";
                y+=2;
            }
            else if (test2)
            {
                niv = 2;
                Console.WriteLine("Tu es arrivé à la tour de boss de niveau {0} !",niv);
                Console.WriteLine("Attention il faut que tu aies de l'espace libre\n" +
                                  "dans ton sac pour avoir les objets du boss !");
                name = "Arthur";
                y+=2;
            }
            else if (test3)
            {
                niv = 1;
                Console.WriteLine("Tu es arrivé à la tour du boss secret !");
                name = "Daimon";
            }
            Console.WriteLine("Que veux-tu faire ?\n 1) Partir\n 2) Sac");
            if (lvl == 0)
            {
                Console.WriteLine(" 3) Allez dans la plaine de niv. {0}",lvl+1);
                y = 28;
            }
            else
            {
                Console.WriteLine(" 3) Se battre");
                y += 29;
            }
            int choix = Ask(1,3, y);
            bool testvie = stats[7] == 2 || stats[7] == 3;
            switch (choix)
            {
                case 1:
                    rp[0] = 0;
                    rp[1] = 0;
                    TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                    break;
                case 2:
                    Sacados(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag);
                    Plaine(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                    break;
                case 3:
                    if (niv!=0)
                    {
                        int fini = Monster.Program.CombatBoss(att,stats,max,exp,ref nbe,atte1,state1,atte2,state2,maxe1,
                        expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,name,niv);
                    NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2);
                    y = 25;
                    if (fini == 1)
                    {
                        switch (name)
                        {
                            case "Arthur":
                                if (max[1]-stats[5]>=2)
                                {
                                    y = 31;
                                    if (niv == 1)
                                    {
                                        Console.WriteLine("Voici une Amélioration de tes statistiques et 1 Os\n" +
                                                          "d'Arthur, 1 Epée de chevalier ainsi que 50 points\n" +
                                                          "d'expérience !");
                                        PaasageNiv(att,stats,max,exp,50);
                                        if (nbe > 0)
                                        {
                                            PaasageNiv(atte1,state1,maxe1,expe1,50);
                                            if (nbe == 2)
                                            {
                                                PaasageNiv(atte2,state2,maxe2,expe2,50);
                                            }
                                        }
                                        quant[2] += 1;
                                        quant[3] += 1;
                                        quant[4] = 1;
                                        Bag.Program.Add(stats,max,bagcount,bag,"Os d'Arthur",1);
                                        Bag.Program.Add(stats,max,bagcount,bag,"Epée de chevalier",1);
                                        rp[2]--;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Voici une Amélioration de tes statistiques et 1 Os\n" +
                                                          "d'Arthur, 1 Epée Légendaire ainsi que 100 points\n" +
                                                          "d'expérience !");
                                        PaasageNiv(att,stats,max,exp,100);
                                        if (nbe > 0)
                                        {
                                            PaasageNiv(atte1,state1,maxe1,expe1,100);
                                            if (nbe == 2)
                                            {
                                                PaasageNiv(atte2,state2,maxe2,expe2,100);
                                            }
                                        }
                                        quant[2] += 1;
                                        quant[3] += 1;
                                        quant[4] = 1;
                                        Bag.Program.Add(stats,max,bagcount,bag,"Os d'Arthur",1);
                                        Bag.Program.Add(stats,max,bagcount,bag,"Epée Légendaire",1);
                                        rp[2]--;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Tu pourras réessayer plus tard avec un sac moins rempli\n" +
                                                      "pour avec les objets du boss ! Tu gagnes quand même 50 d'exp");
                                    PaasageNiv(att,stats,max,exp,50);
                                    if (nbe > 0)
                                    {
                                        PaasageNiv(atte1,state1,maxe1,expe1,50);
                                        if (nbe == 2)
                                        {
                                            PaasageNiv(atte2,state2,maxe2,expe2,50);
                                        }
                                    }
                                    Clicktocont();
                                }
                                break;
                            case "Daimon":
                            {
                                y = 30;
                                Console.WriteLine("Voici une Amélioration de tes statistiques et 1 Corne\n" +
                                                  "du démon ainsi que 450 points d'expérience !");
                                Addstat(stats,0,100,max);
                                Addstat(stats,1,30,max);
                                Addstat(stats,2,30,max);
                                Addstat(stats,3,30,max);
                                Addstat(stats,4,30,max);
                                PaasageNiv(att,stats,max,exp,450);
                                if (nbe > 0)
                                {
                                    Addstat(state1,0,100,maxe1);
                                    Addstat(state1,1,30,maxe1);
                                    Addstat(state1,2,30,maxe1);
                                    Addstat(state1,3,30,maxe1);
                                    Addstat(state1,4,30,maxe1);
                                    PaasageNiv(atte1,state1,maxe1,expe1,450);
                                    if (nbe == 2)
                                    {
                                        Addstat(state2,0,100,maxe2);
                                        Addstat(state2,1,30,maxe2);
                                        Addstat(state2,2,30,maxe2);
                                        Addstat(state2,3,30,maxe2);
                                        Addstat(state2,4,30,maxe2);
                                        PaasageNiv(atte2,state2,maxe2,expe2,450);
                                    }
                                }
                                quant[2] += 10;
                                quant[3] += 10;
                                quant[4] = 1;
                                Bag.Program.Add(stats,max,bagcount,bag,"Corne du démon",1);
                                max[1] += 10;
                                rp[3] --;
                            }
                                break;
                        }
                    }
                    
                    if (stats[0]==0 && !testvie)
                    {
                        GameOver(att,stats,max,exp,nbe, atte1, state1, atte2, state2, maxe1,expe1,maxe2,expe2);

                    }
                    else
                    {
                        if (testvie && stats[0]==0)
                        {
                            stats[0] = max[0];
                            stats[7] = 0;
                        }
                        Console.WriteLine("Que veux-tu faire ?\n 1) Partir");
                        int lim = 1;
                        if (fini == 1)
                        {
                            lim = 2;
                            Console.WriteLine(" 2) Continuer dans ce niveau");
                        }

                        choix = Ask(1,lim, y);
                        if (choix == 1)
                        {
                            TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                        }
                        else
                        {
                            Plaine(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,
                                lvl,rp,rf,rmi,rma,complet); 
                        }
                    }
                    }
                    else
                    {
                        if (lvl == 0)
                        {
                            Plaine(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl+1,rp,rf,rmi,rma,complet);
                        }
                        else
                        {
                            Monster.Program.CombatMonstre(ref nbe, stats, atte1, atte2,state1,state2,maxe1,expe1,maxe2,
                                expe2, bagcount,bag,max,
                                "Blob",rp[0], "MoutMout",rp[1], lvl); //var
                            
                            if (stats[0] == 0 && !testvie)
                            {
                                GameOver(att,stats,max,exp,nbe, atte1, state1, atte2, state2, maxe1,expe1,maxe2,expe2);
                            }
                            else
                            {
                                if (testvie && stats[0]==0)
                                {
                                    stats[0] = max[0];
                                    stats[7] = 0;
                                }
                                int add = (rp[0] + rp[1]) * lvl; //var
                                PaasageNiv(att,stats,max,exp,add);
                                PaasageNiv(atte1,state1,maxe1,expe1,add);
                                PaasageNiv(atte2,state2,maxe2,expe2,add);
                                int loot1 = rnd.Next(1,rp[0]);
                                int loot2=0;
                                if (rp[1] != 0)
                                    loot2 = rp[1];
                                int loot3 = rnd.Next(0,rp[1]);
                                bool garde = true;
                                while (garde)
                                {
                                    NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2);
                                    Console.WriteLine("Tu as eu {0} Slime, {1} Mouton Cru, {2} Laine",loot1,loot2,loot3);
                                    Console.SetCursorPosition(0,24);
                                    Console.WriteLine("Que veux-tu garder ?\n 1) Rien\n 2) Tout\n 3) Slime\n 4) Mouton cru" +
                                                  "\n 5) Laine");
                                    int choix2 = Ask(1,5, 31);
                                    int total = loot1 + loot2 + loot3;
                                    int nb;
                                    switch (choix2) //bug
                                    {
                                        case 1:
                                            garde = false;
                                            break;
                                        case 2:
                                            if (max[1] - stats[5] >= total)
                                            {
                                                Bag.Program.Add(stats,max,bagcount,bag,"Slime", loot1);
                                                Bag.Program.Add(stats,max,bagcount,bag,"Mouton cru", loot2);
                                                Bag.Program.Add(stats,max,bagcount,bag,"Laine", loot3);
                                                garde = false;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Tu n'as pas assez de place.");
                                                Clicktocont();
                                            }
                                            break;
                                        case 3:
                                            Console.WriteLine("Combien en veux-tu ?");
                                            nb = Ask(0,loot1, 33);
                                            if (stats[5] + nb > max[1])
                                            {
                                                Console.WriteLine("Tu n'as pas assez de place.");
                                                Clicktocont();
                                            }
                                            else
                                            { 
                                                Bag.Program.Add(stats,max,bagcount,bag,"Slime", nb);
                                                loot1-=nb;
                                            }
                                            break;
                                        case 4:
                                            Console.WriteLine("Combien en veux-tu ?");
                                            nb = Ask(0,loot2, 33);
                                            if (stats[5] + nb > max[1])
                                            {
                                                Console.WriteLine("Tu n'as pas assez de place.");
                                                Clicktocont();
                                            }
                                            else
                                            {
                                                Bag.Program.Add(stats,max,bagcount,bag,"Mouton cru", nb);
                                                loot2-=nb;
                                            }
                                            break;
                                        case 5:
                                            Console.WriteLine("Combien en veux-tu ?");
                                            nb = Ask(0,loot3, 33);
                                            if (stats[5] + nb > max[1])
                                            {
                                                Console.WriteLine("Tu n'as pas assez de place.");
                                                Clicktocont();
                                            }
                                            else
                                            {
                                                Bag.Program.Add(stats,max,bagcount,bag,"Laine", nb);
                                                loot3-=nb;
                                            }
                                            break;
                                    }
                                }
                                NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                                    atte2, state2, maxe1,expe1,maxe2,expe2);
                                Console.WriteLine("Que veux-tu faire ?\n 1) Partir\n 2) Rester dans ce niveau\n" +
                                              " 3) Allez dans la plaine de niv. {0}",lvl+1);
                                int choix3 = Ask(1,3, 27);
                                if (choix3 == 1) 
                                {
                                    rp[0] = 0;
                                    rp[1] = 0;
                                    TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                                }
                                else if (choix3 == 2)
                                {
                                    rp[0] = 0;
                                    rp[1] = 0;
                                    Plaine(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,
                                        lvl,rp,rf,rmi,rma,complet); 
                                }
                                else
                                {
                                    rp[0] = 0;
                                    rp[1] = 0;
                                    Plaine(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,
                                        lvl+1,rp,rf,rmi,rma,complet);
                                }
                            }
                        }   
                    } //combat monstre ok
                    break;
                
            }
        } //nickel chrome
        
        public static void Foret(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1,
            int[] state1, string[] atte2, int[] state2, int[] maxe1, int[] expe1, int[] maxe2, int[] expe2,
            int[] bagcount, string[] bag, string[] obj, int[] prix, int[] quant, int lvl,int[] rp,int[] rf,
            int[] rmi,int[] rma, int[] complet)
        {
            NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                atte2, state2, maxe1,expe1,maxe2,expe2);
            int y = 0;
            Random rnd= new Random();
            Console.WriteLine("Bienvenue à la Fôret de niveau {0} !", lvl);
            bool test1 = lvl == 5 && rf[2] == 2;
            bool test2 = lvl == 10 && rf[2] == 1;
            bool test3 = lvl == 15 && rf[3] == 1; //fée
            bool test = test1 || test2 || test3;
            bool testinte = stats[3]>=80||state1[3]>=80||state2[3]>=80;
            int niv = 0;
            string name = "";
            bool testvie = stats[7] == 2 || stats[7] == 3;
            if (lvl != 0 && !test)
            {
                if (rf[0] == 0 && rf[0] == 0)
                {
                    int nb1 = rnd.Next(1, lvl);
                    int nb2 = rnd.Next(0, lvl);
                    rf[0] = nb1;
                    rf[1] = nb2;
                }
                Console.WriteLine("Il y a {0} Garou et {1} Vreli", rf[0],rf[1]);
                Affichage.Program.InterfaceMonstre("Garou",lvl,0);
                Affichage.Program.InterfaceMonstre("Vreli",lvl,1);
                Console.SetCursorPosition(0,25);
            }
            else if (test1)
            {
                niv = 1;
                Console.WriteLine("Tu es arrivé à la tour de boss de niveau {0} !",niv);
                Console.WriteLine("Attention il faut que tu aies de l'espace libre\n" +
                                  "dans ton sac pour avoir les objets du boss !");
                name = "Ents";
                y+=2;
            }
            else if (test2)
            {
                niv = 2;
                Console.WriteLine("Tu es arrivé à la tour de boss de niveau {0} !",niv);
                Console.WriteLine("Attention il faut que tu aies de l'espace libre\n" +
                                  "dans ton sac pour avoir les objets du boss !");
                name = "Ents";
                y+=2;
            }
            else if (test3)
            {
                Console.WriteLine("Oh ! Voilà une stèle... Que dit-elle ?");
                y++;
                //fée trouvé un truc -> stele indiquant le besoin de 1 Slime, 1 Peau de Loup,
                //                      1 corne de goblin et peut etre 1 slime visqueux (marais)
            }
            Console.WriteLine("Que veux-tu faire ?\n 1) Partir\n 2) Sac");
            if (lvl == 0)
            {
                Console.WriteLine(" 3) Allez dans la forêt de niv. {0}",lvl+1);
                y = 28;
            }
            else if (test3)
            {
                y += 29;
                if (complet[1]==0)
                    Console.WriteLine(" 3) Lire la stèle");
                else
                {
                    Console.WriteLine(" 3) Prendre la fée (+200 de vie)");
                }
            }
            else
            {
                Console.WriteLine(" 3) Se battre");
                y += 29;
            }
            int choix = Ask(1,3, y);
            switch (choix)
            {
                case 1:
                    rp[0] = 0;
                    rp[1] = 0;
                    TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                    break;
                case 2:
                    Sacados(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag);
                    Foret(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                    break;
                case 3:
                    if (test3)
                    {
                        NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2);
                        if (complet[1] == 0)
                        {
                            if (testinte)
                            {
                                Console.WriteLine("Pour accéder à la Fée et l'avoir en aide, il vous faut\n" +
                                               "apporter 1 de chaque objet suivant :\n" +
                                               "Laine et Peau de Lapin.");
                                if (bagcount[0]>=1 && bagcount[8]>=1)
                                {
                                    Console.WriteLine("Veux-tu les utiliser ?\n 1) Oui      2) Non");
                                    choix = Ask(1, 2, 30);
                                    if (choix == 1)
                                    {
                                        stats[5] -= 2;
                                        bagcount[0] -= 1;
                                        bagcount[8] -= 1;
                                        complet[1] = 1;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Tu n'as aucun des objets demdandé, reviens quand tu les auras !");
                                    Clicktocont();
                                }    
                            }
                            else
                            {
                                Console.WriteLine("Toi et tes équipiers n'avez pas assez d'intelligence, vous\n" +
                                                  "devez avoir un membre avec au moins 80 d'intelligence");
                                Clicktocont();
                            }
                            Foret(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                        }
                        else
                        {
                            stats[7] = 1;
                            rf[3] = 0;
                            Console.WriteLine("Tu as récupéré la fée ! Toute ton équipe bénéficie\n d'un boost de vie" +
                                              "de 200 points de vie.");
                            Addstat(stats,0,200,max);
                            if (nbe > 0)
                            {
                                Addstat(state1,0,200,maxe1);
                                if (nbe == 2)
                                {
                                    Addstat(state1,0,200,maxe1);
                                }
                            }
                            Foret(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                        }
                    }
                    else if (niv!=0)
                    {
                        int fini = Monster.Program.CombatBoss(att,stats,max,exp,ref nbe,atte1,state1,atte2,state2,maxe1,
                        expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,name,niv);
                        NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2);
                    y = 25;
                    if (fini == 1)
                    {
                        switch (name)
                        {
                            case "Ents":
                                if (max[1]-stats[5]>=2)
                                {
                                    y = 31;
                                    if (niv == 1)
                                    {
                                        Console.WriteLine("Voici une Amélioration de tes statistiques et 1 Ecorce\n" +
                                                          "d'Ents, 1 Arc elfique ainsi que 50 points\n" +
                                                          "d'expérience !");
                                        PaasageNiv(att,stats,max,exp,50);
                                        if (nbe > 0)
                                        {
                                            PaasageNiv(atte1,state1,maxe1,expe1,50);
                                            if (nbe == 2)
                                            {
                                                PaasageNiv(atte2,state2,maxe2,expe2,50);
                                            }
                                        }
                                        quant[2] += 1;
                                        quant[3] += 1;
                                        quant[4] = 1;
                                        Bag.Program.Add(stats,max,bagcount,bag,"Ecorce Ents",1);
                                        Bag.Program.Add(stats,max,bagcount,bag,"Arc elfique",1);
                                        rf[2]--;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Voici une Amélioration de tes statistiques et 1 Ecorce\n" +
                                                          "d'Ents, 1 Arc Légendaire ainsi que 100 points\n" +
                                                          "d'expérience !");
                                        PaasageNiv(att,stats,max,exp,100);
                                        if (nbe > 0)
                                        {
                                            PaasageNiv(atte1,state1,maxe1,expe1,100);
                                            if (nbe == 2)
                                            {
                                                PaasageNiv(atte2,state2,maxe2,expe2,100);
                                            }
                                        }
                                        quant[2] += 1;
                                        quant[3] += 1;
                                        quant[4] = 1;
                                        Bag.Program.Add(stats,max,bagcount,bag,"Ecorce Ents",1);
                                        Bag.Program.Add(stats,max,bagcount,bag,"Arc Légendaire",1);
                                        rf[2]--;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Tu pourras réessayer plus tard avec un sac moins rempli\n" +
                                                      "pour avec les objets du boss ! Tu gagnes quand même 50 d'exp");
                                    PaasageNiv(att,stats,max,exp,50);
                                    if (nbe > 0)
                                    {
                                        PaasageNiv(atte1,state1,maxe1,expe1,50);
                                        if (nbe == 2)
                                        {
                                            PaasageNiv(atte2,state2,maxe2,expe2,50);
                                        }
                                    }
                                    Clicktocont();
                                }
                                break;
                        }
                    }
                    
                    if (stats[0]==0 && !testvie)
                    {
                        GameOver(att,stats,max,exp,nbe, atte1, state1, atte2, state2, maxe1,expe1,maxe2,expe2);
                    }
                    else
                    {
                        if (stats[0] == 0 && testvie)
                        {
                            stats[0] = max[0];
                            stats[7] = 0;
                        }
                        Console.WriteLine("Que veux-tu faire ?\n 1) Partir");
                        int lim = 1;
                        if (fini == 1)
                        {
                            lim = 2;
                            Console.WriteLine(" 2) Continuer dans ce niveau");
                        }

                        choix = Ask(1,lim, y);
                        if (choix == 1)
                        {
                            TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                        }
                        else
                        {
                            Foret(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,
                                lvl,rp,rf,rmi,rma,complet); 
                        }
                    }
                    }
                    else
                    {
                        if (lvl == 0)
                        {
                            Foret(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl+1,rp,rf,rmi,rma,complet);
                        }
                        else
                        {
                            Monster.Program.CombatMonstre(ref nbe, stats, atte1, atte2,state1,state2,maxe1,expe1,maxe2,
                                expe2, bagcount,bag,max,
                                "Garou",rf[0], "Vreli",rf[1], lvl); //var
                            if (stats[0]==0 && !testvie)
                                GameOver(att,stats,max,exp,nbe, atte1, state1, atte2, state2, maxe1,expe1,maxe2,expe2);
                            else
                            {
                                if (stats[0] == 0 && testvie)
                                {
                                    stats[0] = max[0];
                                    stats[7] = 0;
                                }
                                int add = (2*rf[0] + rf[1]) * lvl; //var
                                PaasageNiv(att,stats,max,exp,add);
                                PaasageNiv(atte1,state1,maxe1,expe1,add);
                                PaasageNiv(atte2,state2,maxe2,expe2,add);
                                int loot1 = rnd.Next(1,rf[0]);
                                int loot2=0;
                                if (rf[1] != 0)
                                    loot2 = rf[1];
                                int loot3 = rnd.Next(0,rf[1]);
                                bool garde = true;
                                while (garde)
                                {
                                    NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2);
                                    Console.WriteLine("Tu as eu {0} Peau de Loup, {1} Lapin Cru, {2} Peau de Lapin",loot1,loot2,loot3);
                                    Console.SetCursorPosition(0,24);
                                    Console.WriteLine("Que veux-tu garder ?\n 1) Rien\n 2) Tout\n 3) Peau de Loup\n 4) Lapin cru" +
                                                  "\n 5) Peau de Lapin");
                                    int choix2 = Ask(1,5, 31);
                                    int total = loot1 + loot2 + loot3;
                                    int nb;
                                    switch (choix2)
                                    {
                                        case 1:
                                            garde = false;
                                            break;
                                        case 2:
                                            if (max[1] - stats[5] >= total)
                                            {
                                                Bag.Program.Add(stats,max,bagcount,bag,"Peau de Loup", loot1);
                                                Bag.Program.Add(stats,max,bagcount,bag,"Lapin cru", loot2);
                                                Bag.Program.Add(stats,max,bagcount,bag,"Peau de Lapin", loot3);
                                                garde = false;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Tu n'as pas assez de place.");
                                                Clicktocont();
                                            }
                                            break;
                                        case 3:
                                            Console.WriteLine("Combien en veux-tu ?");
                                            nb = Ask(0,loot1, 33);
                                            if (stats[5] + nb > max[1])
                                            {
                                                Console.WriteLine("Tu n'as pas assez de place.");
                                                Clicktocont();
                                            }
                                            else
                                            { 
                                                Bag.Program.Add(stats,max,bagcount,bag,"Peau de Loup", nb);
                                                loot1-=nb;
                                            }
                                            break;
                                        case 4:
                                            Console.WriteLine("Combien en veux-tu ?");
                                            nb = Ask(0,loot2, 33);
                                            if (stats[5] + nb > max[1])
                                            {
                                                Console.WriteLine("Tu n'as pas assez de place.");
                                                Clicktocont();
                                            }
                                            else
                                            {
                                                Bag.Program.Add(stats,max,bagcount,bag,"Lapin cru", nb);
                                                loot2-=nb;
                                            }
                                            break;
                                        case 5:
                                            Console.WriteLine("Combien en veux-tu ?");
                                            nb = Ask(0,loot3, 33);
                                            if (stats[5] + nb > max[1])
                                            {
                                                Console.WriteLine("Tu n'as pas assez de place.");
                                                Clicktocont();
                                            }
                                            else
                                            {
                                                Bag.Program.Add(stats,max,bagcount,bag,"Peau de Lapin", nb);
                                                loot3-=nb;
                                            }
                                            break;
                                    }
                                }
                                NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                                    atte2, state2, maxe1,expe1,maxe2,expe2);
                                Console.WriteLine("Que veux-tu faire ?\n 1) Partir\n 2) Rester dans ce niveau\n" +
                                              " 3) Allez dans la forêt de niv. {0}",lvl+1);
                                int choix3 = Ask(1,3, 27);
                                if (choix3 == 1) 
                                {
                                    rf[0] = 0;
                                    rf[1] = 0;
                                    TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                                }
                                else if (choix3 == 2)
                                {
                                    rf[0] = 0;
                                    rf[1] = 0;
                                    Foret(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,
                                        lvl,rp,rf,rmi,rma,complet); 
                                }
                                else
                                {
                                    rf[0] = 0;
                                    rf[1] = 0;
                                    Foret(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,
                                        lvl+1,rp,rf,rmi,rma,complet);
                                }
                            }
                        }   
                    } //combat monstre ok
                    break;
                
            }
        } //giga ok manque add aide!!!!
        
        public static void Mine(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1,
            int[] state1, string[] atte2, int[] state2, int[] maxe1, int[] expe1, int[] maxe2, int[] expe2,
            int[] bagcount, string[] bag, string[] obj, int[] prix, int[] quant, int lvl,int[] rp,int[] rf,
            int[] rmi,int[] rma,int[] complet)
        {
            NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                atte2, state2, maxe1,expe1,maxe2,expe2);
            int y = 0;
            Random rnd= new Random();
            bool testvie = stats[7] == 2 || stats[7] == 3;
            Console.WriteLine("Bienvenue à la Mine de niveau {0} !", lvl);
            bool test1 = lvl == 5 && rmi[1] == 2;
            bool test2 = lvl == 10 && rmi[1] == 1;
            bool test3 = lvl == 15 && rmi[2] == 1; //Phoenix
            bool test = test1 || test2 || test3;
            bool testinte = stats[3]>=180||state1[3]>=180||state2[3]>=180;
            int niv = 0;
            string name = "";
            if (lvl != 0 && !test)
            {
                if (rmi[0] == 0)
                {
                    int nb1 = rnd.Next(1, lvl+(lvl/2));
                    rmi[0] = nb1;
                }
                Console.WriteLine("Il y a {0} Bline", rmi[0]);
                Affichage.Program.InterfaceMonstre("Bline",lvl,0);
                Console.SetCursorPosition(0,25);
            }
            else if (test1)
            {
                niv = 1;
                Console.WriteLine("Tu es arrivé à la tour de boss de niveau {0} !",niv);
                Console.WriteLine("Attention il faut que tu aies de l'espace libre\n" +
                                  "dans ton sac pour avoir les objets du boss !");
                name = "Colosse";
                y+=2;
            }
            else if (test2)
            {
                niv = 2;
                Console.WriteLine("Tu es arrivé à la tour de boss de niveau {0} !",niv);
                Console.WriteLine("Attention il faut que tu aies de l'espace libre\n" +
                                  "dans ton sac pour avoir les objets du boss !");
                name = "Colosse";
                y+=2;
            }
            else if (test3)
            {
                Console.WriteLine("Oh ! Voilà une stèle... Que dit-elle ?");
                y++;
                //Phoenix -> stele demande 5 mouton cru, 5 lapin cru, 5 poisson rare cru, 1 corne de démon
            }
            Console.WriteLine("Que veux-tu faire ?\n 1) Partir\n 2) Sac");
            if (lvl == 0)
            {
                Console.WriteLine(" 3) Allez dans la mine de niv. {0}",lvl+1);
                y = 28;
            }
            else if (test3)
            {
                y += 29;
                if (complet[1]==0)
                    Console.WriteLine(" 3) Lire la stèle");
                else
                {
                    Console.WriteLine(" 3) Prendre le Phoenix (+1 chance & boost stats)");
                }
            }
            else
            {
                Console.WriteLine(" 3) Se battre");
                y += 29;
            }
            int choix = Ask(1,3, y);
            switch (choix)
            {
                case 1:
                    rmi[0] = 0;
                    TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                    break;
                case 2:
                    Sacados(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag);
                    Mine(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                    break;
                case 3:
                    if (test3)
                    {
                        NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2);
                        if (complet[2] == 0)
                        {
                            if (testinte)
                            {
                                Console.WriteLine("Pour accéder au Phoenix et l'avoir en aide, il vous faut\n" +
                                               "apporter chaque objet suivant : 5 Mouton cru, 5 Lapin cru,\n" +
                                               "5 Poisson rare cru et 1 Corne de démon.");
                                if (bagcount[1]>=5 && bagcount[2]>=5 && bagcount[12]>=5 && bagcount[33]>=1)
                                {
                                    Console.WriteLine("Veux-tu les utiliser ?\n 1) Oui      2) Non");
                                    choix = Ask(1, 2, 30);
                                    if (choix == 1)
                                    {
                                        stats[5] -= 16;
                                        bagcount[1] -= 5;
                                        bagcount[2] -= 5;
                                        bagcount[12] -= 5;
                                        bagcount[33] -= 1;
                                        complet[2] = 1;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Tu n'as aucun des objets demdandé, reviens quand tu les auras !");
                                    Clicktocont();
                                }    
                            }
                            else
                            {
                                Console.WriteLine("Toi et tes équipiers n'avez pas assez d'intelligence, vous\n" +
                                                  "devez avoir un membre avec au moins 180 d'intelligence");
                                Clicktocont();
                            }
                            Mine(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                        }
                        else
                        {
                            stats[7] = 3;
                            rmi[2] = 0;
                            Console.WriteLine("Tu as récupéré le phoenix ! Toute ton équipe bénéficie\n d'un boost des stats" +
                                              "et une chance supplémentaire.");
                            Addstat(stats,0,200,max);
                            Addstat(stats,1,50,max);
                            Addstat(stats,2,50,max);
                            Addstat(stats,3,50,max);
                            Addstat(stats,4,50,max);
                            Addstat(stats,5,50,max);
                            if (nbe > 0)
                            {
                                Addstat(state1,0,200,maxe1);
                                Addstat(state1,1,50,maxe1);
                                Addstat(state1,2,50,maxe1);
                                Addstat(state1,3,50,maxe1);
                                Addstat(state1,4,50,maxe1);
                                if (nbe == 2)
                                {
                                    Addstat(state2,0,200,maxe2);
                                    Addstat(state2,1,50,maxe2);
                                    Addstat(state2,2,50,maxe2);
                                    Addstat(state2,3,50,maxe2);
                                    Addstat(state2,4,50,maxe2);
                                    
                                }
                            }
                            Mine(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                        }
                    }
                    else if (niv!=0)
                    {
                        int fini = Monster.Program.CombatBoss(att,stats,max,exp,ref nbe,atte1,state1,atte2,state2,maxe1,
                        expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,name,niv);
                    NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2);
                    y = 25;
                    if (fini == 1)
                    {
                        switch (name)
                        {
                            case "Colosse":
                                if (max[1]-stats[5]>=2)
                                {
                                    y = 31;
                                    if (niv == 1)
                                    {
                                        Console.WriteLine("Voici une Amélioration de tes statistiques et 1 Roc de\n" +
                                                          "Golem, 1 Hache des nains ainsi que 50 points\n" +
                                                          "d'expérience !");
                                        PaasageNiv(att,stats,max,exp,50);
                                        if (nbe > 0)
                                        {
                                            PaasageNiv(atte1,state1,maxe1,expe1,50);
                                            if (nbe == 2)
                                            {
                                                PaasageNiv(atte2,state2,maxe2,expe2,50);
                                            }
                                        }
                                        quant[2] += 1;
                                        quant[3] += 1;
                                        quant[4] = 1;
                                        Bag.Program.Add(stats,max,bagcount,bag,"Roc de Golem",1);
                                        Bag.Program.Add(stats,max,bagcount,bag,"Hache des nains",1);
                                        rmi[1]--;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Voici une Amélioration de tes statistiques et 1 Roc de\n" +
                                                          "Golem, 1 Hache Légendaire ainsi que 100 points\n" +
                                                          "d'expérience !");
                                        PaasageNiv(att,stats,max,exp,100);
                                        if (nbe > 0)
                                        {
                                            PaasageNiv(atte1,state1,maxe1,expe1,100);
                                            if (nbe == 2)
                                            {
                                                PaasageNiv(atte2,state2,maxe2,expe2,100);
                                            }
                                        }
                                        quant[2] += 1;
                                        quant[3] += 1;
                                        quant[4] = 1;
                                        Bag.Program.Add(stats,max,bagcount,bag,"Roc de Golem",1);
                                        Bag.Program.Add(stats,max,bagcount,bag,"Hache Légendaire",1);
                                        rmi[1]--;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Tu pourras réessayer plus tard avec un sac moins rempli\n" +
                                                      "pour avec les objets du boss ! Tu gagnes quand même 50 d'exp");
                                    PaasageNiv(att,stats,max,exp,50);
                                    if (nbe > 0)
                                    {
                                        PaasageNiv(atte1,state1,maxe1,expe1,50);
                                        if (nbe == 2)
                                        {
                                            PaasageNiv(atte2,state2,maxe2,expe2,50);
                                        }
                                    }
                                    Clicktocont();
                                }
                                break;
                        }
                    }
                    if (stats[0]==0 && !testvie)
                    {
                        GameOver(att,stats,max,exp,nbe, atte1, state1, atte2, state2, maxe1,expe1,maxe2,expe2);

                    }
                    else
                    {
                        if (stats[0] == 0 && testvie)
                        {
                            stats[0] = max[0];
                            stats[7] = 0;
                        }
                        Console.WriteLine("Que veux-tu faire ?\n 1) Partir");
                        int lim = 1;
                        if (fini == 1)
                        {
                            lim = 2;
                            Console.WriteLine(" 2) Continuer dans ce niveau");
                        }

                        choix = Ask(1,lim, y);
                        if (choix == 1)
                        {
                            TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                        }
                        else
                        {
                            Mine(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,
                                lvl,rp,rf,rmi,rma,complet); 
                        }
                    }
                    }
                    else
                    {
                        if (lvl == 0)
                        {
                            Mine(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl+1,rp,rf,rmi,rma,complet);
                        }
                        else
                        {
                            Monster.Program.CombatMonstre(ref nbe, stats, atte1, atte2,state1,state2,maxe1,expe1,maxe2,
                                expe2, bagcount,bag,max,
                                "Bline",rmi[0], "",0, lvl); //var
                            if (stats[0]==0 && !testvie)
                                GameOver(att,stats,max,exp,nbe, atte1, state1, atte2, state2, maxe1,expe1,maxe2,expe2);
                            else
                            {
                                if (stats[0] == 0 && testvie)
                                {
                                    stats[0] = max[0];
                                    stats[7] = 0;
                                }
                                int add = (2*rmi[0]) * lvl; //var
                                PaasageNiv(att,stats,max,exp,add);
                                PaasageNiv(atte1,state1,maxe1,expe1,add);
                                PaasageNiv(atte2,state2,maxe2,expe2,add);
                                int loot1 = rnd.Next(1,rmi[0]);
                                bool garde = true;
                                while (garde)
                                {
                                    NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2);
                                    Console.WriteLine("Tu as eu {0} Dent de goblin",loot1);
                                    Console.SetCursorPosition(0,24);
                                    Console.WriteLine("Que veux-tu garder ?\n 1) Rien\n 2) Tout\n 3) Dent de goblin");
                                    int choix2 = Ask(1,3, 31);
                                    int total = loot1;
                                    int nb;
                                    switch (choix2)
                                    {
                                        case 1:
                                            garde = false;
                                            break;
                                        case 2:
                                            if (max[1] - stats[5] >= total)
                                            {
                                                Bag.Program.Add(stats,max,bagcount,bag,"Dent de goblin", loot1);
                                                garde = false;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Tu n'as pas assez de place.");
                                                Clicktocont();
                                            }
                                            break;
                                        case 3:
                                            Console.WriteLine("Combien en veux-tu ?");
                                            nb = Ask(0,loot1, 33);
                                            if (stats[5] + nb > max[1])
                                            {
                                                Console.WriteLine("Tu n'as pas assez de place.");
                                                Clicktocont();
                                            }
                                            else
                                            { 
                                                Bag.Program.Add(stats,max,bagcount,bag,"Dent de goblin", nb);
                                                loot1-=nb;
                                            }
                                            break;
                                    }
                                }
                                NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                                    atte2, state2, maxe1,expe1,maxe2,expe2);
                                Console.WriteLine("Que veux-tu faire ?\n 1) Partir\n 2) Rester dans ce niveau\n" +
                                              " 3) Allez dans la mine de niv. {0}",lvl+1);
                                int choix3 = Ask(1,3, 27);
                                if (choix3 == 1) 
                                {
                                    rmi[0] = 0;
                                    TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                                }
                                else if (choix3 == 2)
                                {
                                    rmi[0] = 0;
                                    Mine(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,
                                        lvl,rp,rf,rmi,rma,complet); 
                                }
                                else
                                {
                                    rmi[0] = 0;
                                    Mine(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,
                                        lvl+1,rp,rf,rmi,rma,complet);
                                }
                            }
                        }   
                    } //combat monstre ok
                    break;
                
            }
        } //giga ok manque add aide!!!!
        
        public static void Marécages(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1,
            int[] state1, string[] atte2, int[] state2, int[] maxe1, int[] expe1, int[] maxe2, int[] expe2,
            int[] bagcount, string[] bag, string[] obj, int[] prix, int[] quant, int lvl,int[] rp,int[] rf,
            int[] rmi,int[] rma,int[] complet)
        {
            NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                atte2, state2, maxe1,expe1,maxe2,expe2);
            int y = 0;
            Random rnd= new Random();
            Console.WriteLine("Bienvenue à la Marécage de niveau {0} !", lvl);
            bool test1 = lvl == 5 && rma[1] == 2;
            bool test2 = lvl == 10 && rma[1] == 1;
            bool test3 = lvl == 15 && rma[2] == 1; //chat
            bool test = test1 || test2 || test3;
            bool testinte = stats[3]>=130||state1[3]>=130||state2[3]>=130;
            bool testrole = att[1]=="Mage"||atte1[1]=="Mage"||atte2[1]=="Mage"||att[1]=="Elfe"||
                            atte1[1]=="Elfe"||atte2[1]=="Elfe";
            bool testvie = stats[7] == 2 || stats[7] == 3;
            int niv = 0;
            string name = "";
            if (lvl != 0 && !test)
            {
                if (rma[0] == 0)
                {
                    int nb1 = rnd.Next(1, lvl+(lvl/2));
                    rma[0] = nb1;
                }
                Console.WriteLine("Il y a {0} Blob", rma[0]);
                Affichage.Program.InterfaceMonstre("Blob",lvl,0);
                Console.SetCursorPosition(0,25);
            }
            else if (test1)
            {
                niv = 1;
                Console.WriteLine("Tu es arrivé à la tour de boss de niveau {0} !",niv);
                Console.WriteLine("Attention il faut que tu aies de l'espace libre\n" +
                                  "dans ton sac pour avoir les objets du boss !");
                name = "Sorcière";
                y+=2;
            }
            else if (test2)
            {
                niv = 2;
                Console.WriteLine("Tu es arrivé à la tour de boss de niveau {0} !",niv);
                Console.WriteLine("Attention il faut que tu aies de l'espace libre\n" +
                                  "dans ton sac pour avoir les objets du boss !");
                name = "Sorcière";
                y+=2;
            }
            else if (test3)
            {
                Console.WriteLine("Oh ! Voilà une stèle... Que dit-elle ?");
                y++;
                //Chat -> stele demande 5 poisson rare cru -> 1 vie en plus
            }
            Console.WriteLine("Que veux-tu faire ?\n 1) Partir\n 2) Sac");
            if (lvl == 0)
            {
                Console.WriteLine(" 3) Allez dans la marécage de niv. {0}",lvl+1);
                y = 28;
            }
            else if (test3)
            {
                y += 29;
                if (complet[1]==0)
                    Console.WriteLine(" 3) Lire la stèle");
                else
                {
                    Console.WriteLine(" 3) Prendre le Chat (+1 chance & boost stats)");
                }
            }
            else
            {
                Console.WriteLine(" 3) Se battre");
                y += 29;
            }
            int choix = Ask(1,3, y);
            switch (choix)
            {
                case 1:
                    rma[0] = 0;
                    TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                    break;
                case 2:
                    Sacados(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag);
                    Marécages(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                    break;
                case 3:
                    if (test3)
                    {
                        NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2);
                        if (complet[3] == 0)
                        {
                            if (testinte)
                            {
                                Console.WriteLine("Pour accéder au Chat et l'avoir en aide, il vous faut\n" +
                                               "apporter chaque objet suivant 5 Poisson rare cru .");
                                if (bagcount[12]>=5)
                                {
                                    Console.WriteLine("Veux-tu les utiliser ?\n 1) Oui      2) Non");
                                    choix = Ask(1, 2, 30);
                                    if (choix == 1)
                                    {
                                        stats[5] -= 5;
                                        bagcount[12] -= 5;
                                        complet[3] = 1;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Tu n'as aucun des objets demdandé, reviens quand tu les auras !");
                                    Clicktocont();
                                }    
                            }
                            else
                            {
                                Console.WriteLine("Toi et tes équipiers n'avez pas assez d'intelligence, vous\n" +
                                                  "devez avoir un membre avec au moins 130 d'intelligence");
                                Clicktocont();
                            }
                            Marécages(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                        }
                        else
                        {
                            stats[7] = 2;
                            rma[2] = 0;
                            Console.WriteLine("Tu as récupéré le chat ! Toute ton équipe bénéficie\n d'une chance supplémentaire.");
                            
                            Marécages(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl,rp,rf,rmi,rma,complet);
                        }
                    }
                    else if (niv!=0)
                    {
                        if (!testrole)
                        {
                            Console.WriteLine("Tu n'as pas de Mage ou d'Elf dans ton équipe.\nReviens plus tard !");
                            Clicktocont();
                            TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                                atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                        }
                        else
                        {
                            int fini = Monster.Program.CombatBoss(att,stats,max,exp,ref nbe,atte1,state1,atte2,state2,maxe1,
                        expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,name,niv);
                            NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2);
                        y = 25; 
                        if (fini == 1)
                        {
                            switch (name)
                            {
                                case "Sorcière":
                                    if (max[1]-stats[5]>=2)
                                    {
                                        y = 31;
                                        if (niv == 1)
                                        {
                                            Console.WriteLine("Voici une Amélioration de tes statistiques et 1 Chapeau\n" +
                                                              "MagiK, 1 Sceptre de sorcier ainsi que 50 points\n" +
                                                              "d'expérience !");
                                            PaasageNiv(att,stats,max,exp,50);
                                            if (nbe > 0)
                                            {
                                                PaasageNiv(atte1,state1,maxe1,expe1,50);
                                                if (nbe == 2)
                                                {
                                                    PaasageNiv(atte2,state2,maxe2,expe2,50);
                                                }
                                            }
                                            quant[2] += 1;
                                            quant[3] += 1;
                                            quant[4] = 1;
                                            Bag.Program.Add(stats,max,bagcount,bag,"Chapeau MagiK",1);
                                            Bag.Program.Add(stats,max,bagcount,bag,"Sceptre de sorcier",1);
                                            rma[1]--;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Voici une Amélioration de tes statistiques et 1 Chapeau\n" +
                                                              "MagiK, 1 Sceptre Légendaire ainsi que 100 points\n" +
                                                              "d'expérience !");
                                            PaasageNiv(att,stats,max,exp,100);
                                            if (nbe > 0)
                                            {
                                                PaasageNiv(atte1,state1,maxe1,expe1,100);
                                                if (nbe == 2)
                                                {
                                                    PaasageNiv(atte2,state2,maxe2,expe2,100);
                                                }
                                            }
                                            quant[2] += 1;
                                            quant[3] += 1;
                                            quant[4] = 1;
                                            Bag.Program.Add(stats,max,bagcount,bag,"Chapeau MagiK",1);
                                            Bag.Program.Add(stats,max,bagcount,bag,"Sceptre Légendaire",1);
                                            rma[1]--;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Tu pourras réessayer plus tard avec un sac moins rempli\n" +
                                                          "pour avec les objets du boss ! Tu gagnes quand même 50 d'exp");
                                        PaasageNiv(att,stats,max,exp,50);
                                        if (nbe > 0)
                                        {
                                            PaasageNiv(atte1,state1,maxe1,expe1,50);
                                            if (nbe == 2)
                                            {
                                                PaasageNiv(atte2,state2,maxe2,expe2,50);
                                            }
                                        }
                                        Clicktocont();
                                    }
                                    break;
                            }
                        }
                        
                        if (stats[0]==0 && !testvie)
                            GameOver(att,stats,max,exp,nbe, atte1, state1, atte2, state2, maxe1,expe1,maxe2,expe2);
                        else
                        {
                            if (testvie && stats[0]==0)
                            {
                                stats[0] = max[0];
                                stats[7] = 0;
                            }
                            Console.WriteLine("Que veux-tu faire ?\n 1) Partir");
                            int lim = 1;
                            if (fini == 1)
                            {
                                lim = 2;
                                Console.WriteLine(" 2) Continuer dans ce niveau");
                            }

                            choix = Ask(1,lim, y);
                            if (choix == 1)
                            {
                                TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                                    atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                            }
                            else
                            {
                                Marécages(att,stats,max,exp,nbe, atte1, state1, 
                                    atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,
                                    lvl,rp,rf,rmi,rma,complet); 
                            }
                        }
                        }
                    }
                    else
                    {
                        if (lvl == 0)
                        {
                            Marécages(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,lvl+1,rp,rf,rmi,rma,complet);
                        }
                        else
                        {
                            Monster.Program.CombatMonstre(ref nbe, stats, atte1, atte2,state1,state2,maxe1,expe1,maxe2,
                                expe2, bagcount,bag,max,
                                "Blob",rmi[0], "",0, lvl); //var
                            if (stats[0]==0 && !testvie)
                                GameOver(att,stats,max,exp,nbe, atte1, state1, atte2, state2, maxe1,expe1,maxe2,expe2);
                            else
                            {
                                if (testvie && stats[0]==0)
                                {
                                    stats[0] = max[0];
                                    stats[7] = 0;
                                }
                                int add = (2*rma[0]) * lvl; //var
                                PaasageNiv(att,stats,max,exp,add);
                                PaasageNiv(atte1,state1,maxe1,expe1,add);
                                PaasageNiv(atte2,state2,maxe2,expe2,add);
                                int loot1 = rnd.Next(1,rma[0]);
                                bool garde = true;
                                while (garde)
                                {
                                    NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2);
                                    Console.WriteLine("Tu as eu {0} Slime Visqueux",loot1);
                                    Console.SetCursorPosition(0,24);
                                    Console.WriteLine("Que veux-tu garder ?\n 1) Rien\n 2) Tout\n 3) Slime Visqueux");
                                    int choix2 = Ask(1,3, 31);
                                    int total = loot1;
                                    int nb;
                                    switch (choix2)
                                    {
                                        case 1:
                                            garde = false;
                                            break;
                                        case 2:
                                            if (max[1] - stats[5] >= total)
                                            {
                                                Bag.Program.Add(stats,max,bagcount,bag,"Slime Visqueux", loot1);
                                                garde = false;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Tu n'as pas assez de place.");
                                                Clicktocont();
                                            }
                                            break;
                                        case 3:
                                            Console.WriteLine("Combien en veux-tu ?");
                                            nb = Ask(0,loot1, 33);
                                            if (stats[5] + nb > max[1])
                                            {
                                                Console.WriteLine("Tu n'as pas assez de place.");
                                                Clicktocont();
                                            }
                                            else
                                            { 
                                                Bag.Program.Add(stats,max,bagcount,bag,"Slime Visqueux", nb);
                                                loot1-=nb;
                                            }
                                            break;
                                    }
                                }
                                NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                                    atte2, state2, maxe1,expe1,maxe2,expe2);
                                Console.WriteLine("Que veux-tu faire ?\n 1) Partir\n 2) Rester dans ce niveau\n" +
                                              " 3) Allez dans la marécage de niv. {0}",lvl+1);
                                int choix3 = Ask(1,3, 27);
                                if (choix3 == 1) 
                                {
                                    rma[0] = 0;
                                    TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                                }
                                else if (choix3 == 2)
                                {
                                    rma[0] = 0;
                                    Marécages(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,
                                        lvl,rp,rf,rmi,rma,complet); 
                                }
                                else
                                {
                                    rma[0] = 0;
                                    Marécages(att,stats,max,exp,nbe, atte1, state1, 
                                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,
                                        lvl+1,rp,rf,rmi,rma,complet);
                                }
                            }
                        }   
                    } //combat monstre ok
                    break;
                
            }  
        }
        
        public static void GrotteDrag(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1,
            int[] state1, string[] atte2, int[] state2, int[] maxe1, int[] expe1, int[] maxe2, int[] expe2,
            int[] bagcount, string[] bag, string[] obj, int[] prix, int[] quant,int[] rp,int[] rf,
            int[] rmi,int[] rma, int[] complet)
        {
            bool testrole = att[1]=="Mage"||atte1[1]=="Mage"||atte2[1]=="Mage"||att[1]=="Elfe"||
                            atte1[1]=="Elfe"||atte2[1]=="Elfe";
            bool testinte = stats[3]>=200||state1[3]>=200||state2[3]>=200;
            NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                atte2, state2, maxe1,expe1,maxe2,expe2);
            Console.WriteLine("Bienvenue à la Grotte du Dragon !");
            Console.WriteLine("Oh ! Une stèle ! Il y a écrit quelque chose dessus...");
            Console.WriteLine("Que veux-tu faire ?\n 1) Partir\n 2) Sac\n 3) Lire la stèle");
            int y = 29;
            int lim = 3;
            if (testrole && complet[0]==1)
            {
                Console.WriteLine(" 4) Combattre le boss final");
                y++;
                lim++;
            }
            int choix = Ask(1,lim, y);
            switch (choix)
            {
                case 1:
                    TerresElZoria(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                    break;
                case 2:
                    Sacados(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag);
                    GrotteDrag(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                    break;
                case 3:
                    NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2);
                    if (!testinte)
                    {
                        Console.WriteLine("Toi et tes équipiers n'avez pas assez d'intelligence, vous\n" +
                                          "devez avoir un membre avec au moins 200 d'intelligence");
                        Clicktocont();
                    }
                    else
                    {
                        Console.WriteLine("Pour accéder à Dragnir et le combattre, il vous faut avoir\n" +
                                          "au moins 1 membre de votre équipe étant un Elfe ou un Mage\n" +
                                          "ainsi que d'apporter 1 de chaque objet de monstre :\n" +
                                          "Slime, Peau de Loup, Dent de goblin et Slime Visqueux.");
                        if (bagcount[3]>=1 && bagcount[4]>=1 && bagcount[9]>=1 && bagcount[11]>=1)
                        {
                            Console.WriteLine("Veux-tu les utiliser ?\n 1) Oui\n 2) Non");
                            choix = Ask(1, 2, 32);
                            if (choix == 1)
                            {
                                stats[5] -= 4;
                                bagcount[3] -= 1;
                                bagcount[4] -= 1;
                                bagcount[9] -= 1;
                                bagcount[11] -= 1;
                                complet[0] = 1;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Tu n'as aucun des objets demdandé, reviens quand tu les auras !");
                            Clicktocont();
                        }
                    }
                    GrotteDrag(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                    break;
                case 4:
                    int fini = Monster.Program.CombatBoss(att,stats,max,exp,ref nbe,atte1,state1,atte2,state2,maxe1,
                        expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,"Dragnir",1);
                    NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                        atte2, state2, maxe1,expe1,maxe2,expe2);
                    if (stats[0] == 0)
                    {
                        GameOver(att,stats,max,exp,nbe,atte1,state1,atte2,state2,maxe1,expe1,maxe2,expe2);    
                    }
                    else if (fini==1)
                    {
                        Victoire(att,stats,max,exp,nbe,atte1,state1,atte2,state2,maxe1,expe1,maxe2,expe2);
                    }
                    else
                    {
                        GrotteDrag(att,stats,max,exp,nbe, atte1, state1, 
                            atte2, state2, maxe1,expe1,maxe2,expe2,bagcount,bag,obj,prix,quant,rp,rf,rmi,rma,complet);
                    }
                    break;
            }

        }

        public static void GameOver(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1,
            int[] state1, string[] atte2, int[] state2, int[] maxe1, int[] expe1, int[] maxe2, int[] expe2)
        {
            NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                atte2, state2, maxe1,expe1,maxe2,expe2);
            Console.WriteLine("\nGAMEOVER - Tu es mort !\n" +
                              " ---{ Merci d'avoir joué }----\nTu peux recommencer de zéro si tu veux !" +
                              "\nRéalisation du jeu par Matthieu Camart.\n");
            Clicktocont();
        }

        public static void Victoire(string[] att, int[] stats, int[] max, int[] exp, int nbe, string[] atte1,
            int[] state1, string[] atte2, int[] state2, int[] maxe1, int[] expe1, int[] maxe2, int[] expe2)
        {
            NouvelAffichage(att,stats,max,exp,nbe, atte1, state1, 
                atte2, state2, maxe1,expe1,maxe2,expe2);
            Console.WriteLine("\nGAMEOVER - Tu as gagné !\n\n" +
                              " ---{ Merci d'avoir joué }----\nTu peux recommencer de zéro si tu veux !" +
                              "\nRéalisation du jeu par Matthieu Camart.\n");
            Clicktocont();
        }
    }
}