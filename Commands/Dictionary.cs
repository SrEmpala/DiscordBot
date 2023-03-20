using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.Commands
{
    public class Frases
    {
        public Dictionary<int, string> Frase { get; } = new Dictionary<int, string>()
        {
            {1, "Eu amo programar!" },
            {2, "C# é uma excelente linguagem!" },
            {3, "Essa é uma frase aleatória!" },
        };

        private Dictionary<int, string> Playlists { get; } = new Dictionary<int, string>
        {
            {1, "POP" },
            {2, "ROCK" },
            {3, "NACIONAL" },
        };

        public string RandomPlaylist()
        {
            Random rand = new Random();
            int randomIndex = rand.Next(Playlists.Count);
            return Playlists[randomIndex];
        }

        public string RandomMessage()
        {

            Random rand = new Random();
            int randomIndex = rand.Next(Frase.Count);
            return Frase[randomIndex];
        }

    }
}
