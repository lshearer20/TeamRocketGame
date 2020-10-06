using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.Controller
{
    class phaseManager
    {
        public int Index;
        public int Time;
        protected static PhaseManager instance;
        public phases [];

        public EnemyManager enemyMan = new EnemyManager();

        public static PhaseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = init();
                }

                return instance;
            }
        }


        // read the json file and parse it 
        private static PhaseManager init()
        {
            StreamReader reader = GameData.GetPhaseStreamReader();
            string json = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<Phases>(json);
        }

        public static void Reset()
        {
            instance = null;
        }


        public void Update(GameTime gameTime, EnemyManager enemyMan)
        {
            // spawn new eneimes 

        }






    }
}
