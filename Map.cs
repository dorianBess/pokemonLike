using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace pokemonLike
{
    internal class Map
    {
        public const int HEIGHT = 10;
        public const int WIDTH = 10;
        Tile[,] map = new Tile[10, 10];
        public Map() { }
        public void load(string filename)
        {
            // Vérifiez si le fichier JSON existe
            if (File.Exists(filename))
            {
                // Lire le contenu du fichier JSON
                string jsonContent = File.ReadAllText(filename);

                try
                {
                    // Parsez le JSON en un objet JObject
                    JObject jsonObject = JObject.Parse(jsonContent);

                    // Accédez à la valeur de la clé "data" dans chaque layer
                    JArray layers = (JArray)jsonObject["layers"];
                    foreach (JObject layer in layers)
                    {
                        JArray data = (JArray)layer["data"];
                        Console.WriteLine("Valeur de la clé 'data' dans le layer :");
                        int compteur = 0;
                        foreach (int value in data)
                        {
                            switch(value) 
                            {
                                case 1 :
                                    map[compteur / 10,compteur%10] = new WalkTile();
                                    break;
                                case 162:
                                    map[compteur / 10, compteur % 10] = new CombatTile();
                                    break;
                                case 124:
                                    map[compteur / 10, compteur % 10] = new NoneTile();
                                    break;
                                case -1 :
                                    map[compteur / 10, compteur % 10] = new MarchandTile();
                                    break;
                                case 2:
                                    map[compteur / 10, compteur % 10] = new HealCenterTile();
                                    break;
                            }
                            compteur++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la lecture du JSON : " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Le fichier JSON spécifié n'existe pas.");
            }
        }

        public void show(Vector2Int playerPos)
        {
            Console.Write("\n");
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    if(playerPos.x == x && playerPos.y == y)
                    {
                        Console.Write(" P ");
                    }
                    else 
                    {
                        this.map[x, y].showTile();
                    }                    
                }
                Console.Write("\n");
            }
        }

        public TypeTile GetTypeTile(int x, int y) 
        {
            return map[x, y].type;
        }

        public TypeTile GetTypeTile(Vector2Int pos)
        {
            return map[pos.x, pos.y].type;
        }

        public Tile GetTile(int x, int y) 
        {
            return map[x, y];
        }

        public Tile GetTile(Vector2Int pos)
        {
            return map[pos.x, pos.y];
        }
    }
}
