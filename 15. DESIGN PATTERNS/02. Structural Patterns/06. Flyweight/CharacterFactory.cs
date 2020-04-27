namespace _06._Flyweight
{
    using System.Collections.Generic;

    /// <summary>
    /// The 'FlyweightFactory' class
    /// </summary>
    public class CharacterFactory
    {
        private readonly Dictionary<char, Character> _characters;

        public CharacterFactory()
        {
            this._characters = new Dictionary<char, Character>();
        }

        public Character GetCharacter(char key)
        {
            // Uses "lazy initialization"
            Character character = null;

            if (this._characters.ContainsKey(key))
            {
                character = this._characters[key];
            }
            else
            {
                switch (key)
                {
                    case 'A': character = new CharacterA(); break;
                    case 'B': character = new CharacterB(); break;
                    //...

                    case 'Z': character = new CharacterZ(); break;
                }

                this._characters.Add(key, character);
            }

            return character;
        }
    }
}
