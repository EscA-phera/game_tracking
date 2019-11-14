/*!
 * Copyright (c) 2019 - 2020, Team NACT, made by esca.phera@esphera.xyz
 */

using System.ServiceProcess;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Win32;
using System.Text;

namespace game_tracking
{
    public partial class Service1 : ServiceBase
    {

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ThreadStart GameTrackingThread = new ThreadStart(this.game_tracking_thread);
                Thread GameThread = new Thread(GameTrackingThread);

                GameThread.Start();
            }

            catch(ThreadStartException ThreadedFailed)
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://dkanrjsk.paas-ta.org/debug");
                var response = (HttpWebResponse)httpWebRequest.GetResponse();
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";

                RegistryKey regKey = Registry.CurrentUser;
                regKey = regKey.OpenSubKey("IKnowEscAsMind\\BecauseItsFailed", true);
                object clientID = regKey.GetValue("ClientID");

                try
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = new JavaScriptSerializer().Serialize(new
                        {
                            clientid = clientID.ToString(),
                            status = ThreadedFailed.ToString()
                        });

                        httpWebRequest.ContentLength = json.Length;
                        streamWriter.Write(json);
                    }
                }

                catch (HttpListenerException HttpFailed)
                {
                    string savePath = @"\FailedWhatisYourWeb";
                    string textValue = ("인터넷 연결 오류\n\n" 
                        + HttpFailed.ToString() 
                        + "\n\n" 
                        + "실행 오류\n\n"
                        + ThreadedFailed.ToString()
                        );

                    File.WriteAllText(savePath, textValue, Encoding.Default);

                    EventLog.WriteEntry("오류가 발생하였습니다. 인터넷 연결을 다시 한 번 확인하여 주세요.");
                }

                EventLog.WriteEntry("오류가 발생하였습니다. 램 용량을 확보하신 후 다시 설치하여 주세요.");
            }

            catch (ThreadStateException ThreadedStateError)
            {
                string savePath = @"\FailedStateError";
                string textValue = ("램 용량 오류\n\n"
                    + ThreadedStateError.ToString()
                    );

                File.WriteAllText(savePath, textValue, Encoding.Default);

                EventLog.WriteEntry("오류가 발생하였습니다. 램 용량이 충분한 지 다시 한 번 확인하여 주세요.");
            }
        }

        protected override void OnStop()
        {
            Process.Start("shutdown.exe", "-r");
        }
        
        private void game_tracking_thread()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://dkanrjsk.paas-ta.org/game"); // paas-ta 공모전 작품
            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST"; // sending type = POST

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("IKnowEscAsMind\\BecauseItsFailed", true);
            string client_id = regKey.GetValue("ClientID").ToString();
            string Clientid = client_id;

            List<string> game_list = new List<string>(); // string d_array using list of STL

            Process[] rainbowsix = Process.GetProcessesByName("rainbowsix"); // gamename rainbow six - siege
            Process[] csgo = Process.GetProcessesByName("csgo"); // gamename counter strike - global offensive
            Process[] league_of_legend = Process.GetProcessesByName("lolclient"); // gamename League Of Legend
            Process[] world_of_warcraft = Process.GetProcessesByName("WorldOfWarcraft"); // gamename World Of Warcraft
            Process[] keep_talking_and_nobody_explodes = Process.GetProcessesByName("ktane"); // gamename Keep Talking and Nobody Explodes
            Process[] toxikk = Process.GetProcessesByName("TOXIKK"); // gamename TOXIKK
            Process[] one_finger_death_punch = Process.GetProcessesByName("One Finger Death Punch"); // gamename One Finger Death punch
            Process[] battle_vs_chess = Process.GetProcessesByName("battlevschess"); // gamename Battle Vs Chess
            Process[] castle_crashers = Process.GetProcessesByName("castle"); // gamename Castle Crashers
            Process[] dirt_3_complete_edition = Process.GetProcessesByName("dirt3_game"); // gamename Dirt 3 Complete Edition
            Process[] robot_roller_derby_disco_dodgeball = Process.GetProcessesByName("Disco Dodgeball"); // gamename Robot Roller-Derby Disco Dodgeball
            Process[] dont_starve_together = Process.GetProcessesByName("dontstarve_steam"); // gamename Don't Starve Together
            Process[] dungeon_souls = Process.GetProcessesByName("DungeonSouls"); // gamename Dungeon Souls
            Process[] euro_truck_simulator_2 = Process.GetProcessesByName("eurotrucks2"); // gamename Euro Truck Simulator
            Process[] final_exam = Process.GetProcessesByName("final_exam"); // gamename Final Exam
            Process[] full_mojo_rampage = Process.GetProcessesByName("FullMojo"); // gamename Full Mojo Rampage
            Process[] guacamelee = Process.GetProcessesByName("Guac"); // gamename guacamelee
            Process[] hammerwatch = Process.GetProcessesByName("Hammerwatch"); // gamename Hammerwatch
            Process[] hero_siege = Process.GetProcessesByName("Hero_Siege"); // gamename Hero Siege
            Process[] the_king_of_fighters_8 = Process.GetProcessesByName("kofxiii"); // gamename King Of Fighters XIII
            Process[] sudden_attack = Process.GetProcessesByName("suddenattack"); // gamename Sudden Attack
            Process[] overwatch = Process.GetProcessesByName("overwatch"); // gamename Overwatch
            Process[] elsword = Process.GetProcessesByName("Elsword"); // gamename Elsword
            Process[] magicka_wizard_wars = Process.GetProcessesByName("WizardWarsLauncher"); // gamename Magicka Wizard Wars
            Process[] not_the_robots = Process.GetProcessesByName("Not the Robots"); // gamename Not the Robots
            Process[] path_of_exile = Process.GetProcessesByName("PathOfExileSteam"); // gamename Path of Exile
            Process[] quake_live = Process.GetProcessesByName("quakelive_steam"); // gamename Quake Live
            Process[] sir_you_are_being_hunted = Process.GetProcessesByName("sir"); // gamename Sir You Are Being Hunted
            Process[] speed_runners = Process.GetProcessesByName("SpeedRunners"); // gamename SpeedRunners
            Process[] star_bound = Process.GetProcessesByName("starbound"); // gamename Starbound
            Process[] star_bound_opengl = Process.GetProcessesByName("starbound_opengl"); // gamename Starbound
            Process[] tabletop_simulator = Process.GetProcessesByName("Tabletop SImulator"); // gamename Tabletop Simulator
            Process[] toribash = Process.GetProcessesByName("toribash"); // gamename Toribash
            Process[] unturned = Process.GetProcessesByName("Unturned"); // gamename Unturned
            Process[] war_thunder = Process.GetProcessesByName("aces"); // gamename War Thunder
            Process[] arma_2 = Process.GetProcessesByName("arma2"); // gamename ArmA 2
            Process[] arma_2_operation_arrow_head = Process.GetProcessesByName("ArmA2OA"); // gamename ArmA 2: Operation Arrowhead
            Process[] arma_3 = Process.GetProcessesByName("arma3"); // gamename ArmA 3
            Process[] mania_planet = Process.GetProcessesByName("maniaplanet"); // gamename Maniaplanet
            Process[] broforce = Process.GetProcessesByName("broforce_beta"); // gamename Broforce
            Process[] heroes_and_generals = Process.GetProcessesByName("HeroesAndHeneralsDesktop"); // gamename Heroes & Generals
            Process[] heroes_and_generals_real = Process.GetProcessesByName("hng"); // gamename Heroes & Generals Real
            Process[] downwell = Process.GetProcessesByName("downwell"); // gamename Downwell
            Process[] rebel_galaxy = Process.GetProcessesByName("RebelGalaxy"); // gamename Rebel Galaxy
            Process[] rebel_galaxy_real = Process.GetProcessesByName("RebelGalaxyGOG"); // gamename Rebel Galaxy GOG
            Process[] warhammer_end_times_vermintide = Process.GetProcessesByName("vermintide"); // gamename Warhammer End Times: Vermintide
            Process[] star_trek_online = Process.GetProcessesByName("Star_Trek_Online"); // gamename Star Trek Online
            Process[] scrivener = Process.GetProcessesByName("Scrivener"); // gamename scrivener
            Process[] delta_force_black_hawk_down = Process.GetProcessesByName("dfbhd"); // gamename Delta Force Black Hawk Down
            Process[] total_war_attila = Process.GetProcessesByName("Atilla"); // gamename Total War Attila
            Process[] bastion = Process.GetProcessesByName("Bastion"); // gamename Bastion
            Process[] blood_bowl_2 = Process.GetProcessesByName("BloodBowl2"); // gamename Blood Bowl 2
            Process[] blood_bowl_2_dx_32 = Process.GetProcessesByName("BloodBowl2_DX_32"); // gamename Blood Bowl 2 DX_32
            Process[] blood_bowl_2_gl_32 = Process.GetProcessesByName("BloodBowl2_GL_32"); // gamename Blood Bowl 2 GL_32
            Process[] borderlands = Process.GetProcessesByName("Borderlands"); // gamename Borderlands

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                try
                {
                    string result = streamReader.ReadToEnd();
                    string WantResult = "successfully generated";
                    bool StringExists = result.Contains(WantResult);

                    if (StringExists == true)
                    {
                        goto check_process;
                    }

                    else
                    {
                        goto check_process;
                    }
                }

                catch (HttpListenerException HttpError)
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = new JavaScriptSerializer().Serialize(new
                        {
                            clientid = Clientid,
                            status = HttpError.ToString()
                        });

                        httpWebRequest.ContentLength = json.Length;
                        streamWriter.Write(json);
                    }

                    EventLog.WriteEntry("시스템을 삭제하신 후 문의하여 주세요. esca.phera@esphera.xyz");
                }
            }


        check_process:
            while (true)
            {
                rainbowsix = Process.GetProcessesByName("rainbowsix"); string rainbow_six = "rainbowsix";
                string result_r = game_list.Find(delegate (string data) { return (rainbow_six == data); });

                csgo = Process.GetProcessesByName("csgo"); string CsGo = "counter_strike_global_offensive";
                string result_c = game_list.Find(delegate (string data) { return (CsGo == data); });

                league_of_legend = Process.GetProcessesByName("lolclient"); string LeagueOfLegend = "league_of_legend";
                string result_l = game_list.Find(delegate (string data) { return (LeagueOfLegend == data); });

                world_of_warcraft = Process.GetProcessesByName("WorldOfWarcraft"); string WorldOfWarcraft = "world_of_warcraft";
                string result_w = game_list.Find(delegate (string data) { return (WorldOfWarcraft == data); });

                keep_talking_and_nobody_explodes = Process.GetProcessesByName("ktane"); string KeepTalkingAndNobodyExplodes = "keep_talking_and_nobody_explodes";
                string result_k = game_list.Find(delegate (string data) { return (KeepTalkingAndNobodyExplodes == data); });

                toxikk = Process.GetProcessesByName("TOXIKK"); string to_xikk = "toxikk";
                string result_t = game_list.Find(delegate (string data) { return (to_xikk == data); });

                one_finger_death_punch = Process.GetProcessesByName("One FInger Death Punch"); string OneFingerDeathPunch = "one_finger_death_punch";
                string result_o = game_list.Find(delegate (string data) { return (OneFingerDeathPunch == data); });

                battle_vs_chess = Process.GetProcessesByName("battlevschess"); string BattleVsChess = "battle_vs_chess";
                string result_b = game_list.Find(delegate (string data) { return (BattleVsChess == data); });

                castle_crashers = Process.GetProcessesByName("castle"); string CastleCrashers = "castle_crashes";
                string result_ca = game_list.Find(delegate (string data) { return (CastleCrashers == data); });

                dirt_3_complete_edition = Process.GetProcessesByName("dirt3_game"); string Dirt3CompleteEdition = "dirt_3_complete_editon";
                string result_di = game_list.Find(delegate (string data) { return (Dirt3CompleteEdition == data); });

                robot_roller_derby_disco_dodgeball = Process.GetProcessesByName("Disco Dodgeball"); string RobotRollerDerbyDiscoDodgeball = "robot_roller_derby_disco_odgeball";
                string result_ro = game_list.Find(delegate (string data) { return (RobotRollerDerbyDiscoDodgeball == data); });

                dont_starve_together = Process.GetProcessesByName("dontstarve_steam"); string DontStarveTogehter = "dont_starve_together";
                string result_do = game_list.Find(delegate (string data) { return (DontStarveTogehter == data); });

                dungeon_souls = Process.GetProcessesByName("DungeonSouls"); string DungeonSouls = "dungeon_souls";
                string result_du = game_list.Find(delegate (string data) { return (DungeonSouls == data); });

                euro_truck_simulator_2 = Process.GetProcessesByName("eurotrucks2"); string EuroTruckSImulator2 = "euro_truck_simulator_2";
                string result_eu = game_list.Find(delegate (string data) { return (EuroTruckSImulator2 == data); });

                final_exam = Process.GetProcessesByName("fianl_exam"); string FinalExam = "final_exam";
                string result_fi = game_list.Find(delegate (string data) { return (FinalExam == data); });

                full_mojo_rampage = Process.GetProcessesByName("FullMojo"); string FullMojoRampage = "full_mojo_rampage";
                string result_fu = game_list.Find(delegate (string data) { return (FullMojoRampage == data); });

                guacamelee = Process.GetProcessesByName("Guac"); string GuaCameLee = "guacamelee";
                string result_gu = game_list.Find(delegate (string data) { return (GuaCameLee == data); });

                hammerwatch = Process.GetProcessesByName("Hammerwatch"); string HammerWatch = "hammerwatch";
                string result_ha = game_list.Find(delegate (string data) { return (HammerWatch == data); });

                hero_siege = Process.GetProcessesByName("Hero_Siege"); string HeroSiege = "hero_siege";
                string result_he = game_list.Find(delegate (string data) { return (HeroSiege == data); });

                the_king_of_fighters_8 = Process.GetProcessesByName("kofxiii"); string TheKingOfFighters8 = "the_king_of_fighters_8";
                string result_th = game_list.Find(delegate (string data) { return (TheKingOfFighters8 == data); });

                magicka_wizard_wars = Process.GetProcessesByName("WizardWarsLauncher"); string MagickaWizardWars = "magicka_wizard_wars";
                string result_ma = game_list.Find(delegate (string data) { return (MagickaWizardWars == data); });

                sudden_attack = Process.GetProcessesByName("suddenattack"); string SuddenAttack = "sudden_attack";
                string result_su = game_list.Find(delegate (string data) { return (SuddenAttack == data); });

                overwatch = Process.GetProcessesByName("overwatch"); string OverWatch = "over_watch";
                string result_ov = game_list.Find(delegate (string data) { return (OverWatch == data); });

                elsword = Process.GetProcessesByName("elsword"); string Elsword = "elsword";
                string result_el = game_list.Find(delegate (string data) { return (Elsword == data); });

                not_the_robots = Process.GetProcessesByName("Not the Robots"); string NotTheRobots = "not_the_robots";
                string result_no = game_list.Find(delegate (string data) { return (NotTheRobots == data); });

                path_of_exile = Process.GetProcessesByName("PathOfExileSteam"); string PathOfExile = "path_of_exile";
                string result_pa = game_list.Find(delegate (string data) { return (PathOfExile == data); });

                quake_live = Process.GetProcessesByName("quakelive_steam"); string QuakeLive = "quake_live";
                string result_qu = game_list.Find(delegate (string data) { return (QuakeLive == data); });

                sir_you_are_being_hunted = Process.GetProcessesByName("sir"); string SirYouAreBeingHunted = "sir_you_are_being_hunted";
                string result_si = game_list.Find(delegate (string data) { return (SirYouAreBeingHunted == data); });

                speed_runners = Process.GetProcessesByName("SpeedRunners"); string SpeedRunners = "speed_runners";
                string result_sp = game_list.Find(delegate (string data) { return (SpeedRunners == data); });

                star_bound = Process.GetProcessesByName("starbound"); string StarBound = "star_bound";
                string result_st = game_list.Find(delegate (string data) { return (StarBound == data); });

                star_bound_opengl = Process.GetProcessesByName("starbound_opengl"); string StarBoundOpenGL = "star_bound_opengl";
                string result_st_opengl = game_list.Find(delegate (string data) { return (StarBoundOpenGL == data); });

                tabletop_simulator = Process.GetProcessesByName("Tabletop Simulator"); string TabletopSimulator = "tabletop_simulator";
                string reuslt_ta = game_list.Find(delegate (string data) { return (TabletopSimulator == data); });

                toribash = Process.GetProcessesByName("toribash"); string Toribash = "toribash";
                string result_to = game_list.Find(delegate (string data) { return (Toribash == data); });

                unturned = Process.GetProcessesByName("Unturned"); string Unturned = "unturned";
                string result_un = game_list.Find(delegate (string data) { return (Unturned == data); });

                war_thunder = Process.GetProcessesByName("aces"); string WarThunder = "war_thunder";
                string result_wa = game_list.Find(delegate (string data) { return (WarThunder == data); });

                arma_2 = Process.GetProcessesByName("arma2"); string Arma2 = "arma_2";
                string result_ar = game_list.Find(delegate (string data) { return (Arma2 == data); });

                arma_2_operation_arrow_head = Process.GetProcessesByName("ArmA2OA"); string Arma2OpertaionArrowHead = "arma_2_operation_arrow_head";
                string result_ar2 = game_list.Find(delegate (string data) { return (Arma2OpertaionArrowHead == data); });

                arma_3 = Process.GetProcessesByName("arma3"); string Arma3 = "arma_3";
                string result_ar3 = game_list.Find(delegate (string data) { return (Arma3 == data); });

                mania_planet = Process.GetProcessesByName("maniaplanet"); string ManiaPlanet = "mania_planet";
                string result_man = game_list.Find(delegate (string data) { return (ManiaPlanet == data); });

                broforce = Process.GetProcessesByName("broforce_beta"); string Broforce = "broforce";
                string result_br = game_list.Find(delegate (string data) { return (Broforce == data); });

                heroes_and_generals = Process.GetProcessesByName("HeroesAndHeneralsDesktop"); string HeroesAndGenerals = "heroes_and_generals";
                string result_her = game_list.Find(delegate (string data) { return (HeroesAndGenerals == data); });

                heroes_and_generals_real = Process.GetProcessesByName("hng"); string HeroesAndGeneralsReal = "heroes_and_generals";
                string result_her_real = game_list.Find(delegate (string data) { return (HeroesAndGeneralsReal == data); });

                downwell = Process.GetProcessesByName("downwell"); string DownWell = "downwell";
                string result_dow = game_list.Find(delegate (string data) { return (DownWell == data); });

                rebel_galaxy = Process.GetProcessesByName("RebelGalaxy"); string RebelGalaxy = "rebel_galaxy";
                string result_re = game_list.Find(delegate (string data) { return (RebelGalaxy == data); });

                rebel_galaxy_real = Process.GetProcessesByName("RebelGalaxyGOG"); string RebelGalaxyGOG = "rebel_galaxy_GOG";
                string result_reb = game_list.Find(delegate (string data) { return (RebelGalaxyGOG == data); });

                warhammer_end_times_vermintide = Process.GetProcessesByName("vermintide"); string WarhammerEndTimesVermintide = "Warhammer_End_Times_Vermintide";
                string result_war = game_list.Find(delegate (string data) { return (WarhammerEndTimesVermintide == data); });

                star_trek_online = Process.GetProcessesByName("Star_Trek_Online"); string StarTrekOnline = "Star_Trek_Online";
                string result_sta = game_list.Find(delegate (string data) { return (StarTrekOnline == data); });

                scrivener = Process.GetProcessesByName("Scrivener"); string Scrivener = "Scrivener";
                string result_sc = game_list.Find(delegate (string data) { return (Scrivener == data); });

                delta_force_black_hawk_down = Process.GetProcessesByName("dfbhd"); string DeltaForceBlackHawkDown = "delta_force_black_hawk_down";
                string result_de = game_list.Find(delegate (string data) { return (DeltaForceBlackHawkDown == data); });

                total_war_attila = Process.GetProcessesByName("Atilla"); string TotalWarAttila = "total_war_attila";
                string result_tot = game_list.Find(delegate (string data) { return (TotalWarAttila == data); });

                bastion = Process.GetProcessesByName("Bastion"); string Bastion = "Bastion";
                string result_ba = game_list.Find(delegate (string data) { return (Bastion == data); });

                blood_bowl_2 = Process.GetProcessesByName("BloodBowl2"); string BloodBowl2 = "blood_bowl_2";
                string result_bl = game_list.Find(delegate (string data) { return (BloodBowl2 == data); });

                blood_bowl_2_dx_32 = Process.GetProcessesByName("BloodBowl2_DX_32"); string BloodBowl2DX32 = "blood_bowl_2_dx_32";
                string result_bl_dx = game_list.Find(delegate (string data) { return (BloodBowl2DX32 == data); });

                blood_bowl_2_gl_32 = Process.GetProcessesByName("BloodBowl2_GL_32"); string BloodBowl2GL32 = "blood_Bowl_2_gl_32";
                string result_bl_gl = game_list.Find(delegate (string data) { return (BloodBowl2GL32 == data); });

                borderlands = Process.GetProcessesByName("Borderlands"); string BorderLands = "borderlands";
                string result_bor = game_list.Find(delegate (string data) { return (BorderLands == data); });

                // rainbow_six_siege game checking
                try
                {
                    if ((rainbowsix.Length >= 1) && (result_r == null))
                    {
                        game_list.Add(rainbow_six);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = rainbow_six
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((rainbowsix.Length < 1) && (result_r != null))
                    {
                        game_list.Remove(rainbow_six);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = rainbow_six
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    // counter_strike_global_offensive game checking
                    else if ((csgo.Length >= 1) && (result_c == null))
                    {
                        game_list.Add(CsGo);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = CsGo
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((csgo.Length < 1) && (result_c != null))
                    {
                        game_list.Remove(CsGo);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = CsGo
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((league_of_legend.Length >= 1) && (result_l == null))
                    {
                        game_list.Add(LeagueOfLegend);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = LeagueOfLegend
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((league_of_legend.Length < 1) && (result_l != null))
                    {
                        game_list.Remove(LeagueOfLegend);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = LeagueOfLegend
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((world_of_warcraft.Length >= 1) && (result_w == null))
                    {
                        game_list.Add(WorldOfWarcraft);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = WorldOfWarcraft
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((world_of_warcraft.Length < 1) && (result_w != null))
                    {
                        game_list.Remove(WorldOfWarcraft);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = WorldOfWarcraft
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((keep_talking_and_nobody_explodes.Length >= 1) && (result_k == null))
                    {
                        game_list.Add(KeepTalkingAndNobodyExplodes);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = KeepTalkingAndNobodyExplodes
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((keep_talking_and_nobody_explodes.Length < 1) && (result_k != null))
                    {
                        game_list.Remove(KeepTalkingAndNobodyExplodes);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = KeepTalkingAndNobodyExplodes
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((toxikk.Length >= 1) && (result_t == null))
                    {
                        game_list.Add(to_xikk);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = to_xikk
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((toxikk.Length < 1) && (result_t != null))
                    {
                        game_list.Remove(to_xikk);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = to_xikk
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((one_finger_death_punch.Length >= 1) && (result_o == null))
                    {
                        game_list.Add(OneFingerDeathPunch);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = OneFingerDeathPunch
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((one_finger_death_punch.Length < 1) && (result_o != null))
                    {
                        game_list.Remove(OneFingerDeathPunch);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = OneFingerDeathPunch
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((battle_vs_chess.Length >= 1) && (result_b == null))
                    {
                        game_list.Add(BattleVsChess);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = BattleVsChess
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((battle_vs_chess.Length < 1) && (result_b != null))
                    {
                        game_list.Remove(BattleVsChess);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = BattleVsChess
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((castle_crashers.Length >= 1) && (result_c == null))
                    {
                        game_list.Add(CastleCrashers);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = CastleCrashers
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((castle_crashers.Length < 1) && (result_c != null))
                    {
                        game_list.Remove(CastleCrashers);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = CastleCrashers
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((dirt_3_complete_edition.Length >= 1) && (result_di == null))
                    {
                        game_list.Add(Dirt3CompleteEdition);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = Dirt3CompleteEdition
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((dirt_3_complete_edition.Length < 1) && (result_di != null))
                    {
                        game_list.Remove(Dirt3CompleteEdition);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = Dirt3CompleteEdition
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((robot_roller_derby_disco_dodgeball.Length >= 1) && (result_ro == null))
                    {
                        game_list.Add(RobotRollerDerbyDiscoDodgeball);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = RobotRollerDerbyDiscoDodgeball
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((robot_roller_derby_disco_dodgeball.Length < 1) && (result_ro != null))
                    {
                        game_list.Remove(RobotRollerDerbyDiscoDodgeball);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = RobotRollerDerbyDiscoDodgeball
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((dont_starve_together.Length >= 1) && (result_do == null))
                    {
                        game_list.Add(DontStarveTogehter);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = DontStarveTogehter
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((dont_starve_together.Length < 1) && (result_do != null))
                    {
                        game_list.Remove(DontStarveTogehter);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = DontStarveTogehter
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((dungeon_souls.Length >= 1) && (result_du == null))
                    {
                        game_list.Add(DungeonSouls);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = DungeonSouls
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((dungeon_souls.Length < 1) && (result_du != null))
                    {
                        game_list.Remove(DungeonSouls);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = DungeonSouls
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((euro_truck_simulator_2.Length >= 1) && (result_eu == null))
                    {
                        game_list.Add(EuroTruckSImulator2);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = EuroTruckSImulator2
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((euro_truck_simulator_2.Length < 1) && (result_eu != null))
                    {
                        game_list.Remove(EuroTruckSImulator2);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = EuroTruckSImulator2
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((final_exam.Length >= 1) && (result_fi == null))
                    {
                        game_list.Add(FinalExam);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = FinalExam
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((final_exam.Length < 1) && (result_ro != null))
                    {
                        game_list.Remove(FinalExam);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = FinalExam
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((full_mojo_rampage.Length >= 1) && (result_fu == null))
                    {
                        game_list.Add(FullMojoRampage);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = FullMojoRampage
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((full_mojo_rampage.Length < 1) && (result_fu != null))
                    {
                        game_list.Remove(FullMojoRampage);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = FullMojoRampage
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((GuaCameLee.Length >= 1) && (result_gu == null))
                    {
                        game_list.Add(GuaCameLee);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = GuaCameLee
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((GuaCameLee.Length < 1) && (result_gu != null))
                    {
                        game_list.Remove(GuaCameLee);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = GuaCameLee
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((hammerwatch.Length >= 1) && (result_ha == null))
                    {
                        game_list.Add(HammerWatch);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = HammerWatch
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamWriter.Write(json);
                        }

                        continue;
                    }

                    else if ((hammerwatch.Length < 1) && (result_ha != null))
                    {
                        game_list.Remove(HammerWatch);

                        using (var streamwriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = HammerWatch
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamwriter.Write(json);
                        }

                        continue;
                    }

                    else if ((hero_siege.Length >= 1) && (result_he == null))
                    {
                        game_list.Add(HeroSiege);

                        using (var streamwriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = HeroSiege
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamwriter.Write(json);
                        }

                        continue;
                    }

                    else if ((hero_siege.Length < 1) && (result_he != null))
                    {
                        game_list.Remove(HeroSiege);

                        using (var streamwriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = HeroSiege
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamwriter.Write(json);
                        }

                        continue;
                    }

                    else if ((the_king_of_fighters_8.Length >= 1) && (result_th == null))
                    {
                        game_list.Add(TheKingOfFighters8);

                        using (var streamwriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = TheKingOfFighters8
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamwriter.Write(json);
                        }

                        continue;
                    }

                    else if ((the_king_of_fighters_8.Length < 1) && (result_th == null))
                    {
                        game_list.Remove(TheKingOfFighters8);

                        using (var streamwriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = TheKingOfFighters8
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamwriter.Write(json);
                        }

                        continue;
                    }

                    else if ((sudden_attack.Length < 1) && (result_su == null))
                    {
                        game_list.Add(SuddenAttack);

                        using (var streamwriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                clientid = Clientid,
                                gamename = SuddenAttack
                            });

                            httpWebRequest.ContentLength = json.Length;
                            streamwriter.Write(json);
                        }

                        continue;
                    }

                    else
                    {
                        continue;
                    }
                }

                catch(HttpListenerException HttpError)
                {
                    string savePath = @"\FailedWhatisYourWeb";
                    string textValue = ("인터넷 연결 오류\n\n"
                        + HttpError.ToString()
                        );

                    File.WriteAllText(savePath, textValue, Encoding.Default);
                }
            }
        }
    }
}