﻿namespace Automapper
{
    public class MapperConfiguration
    {
       public Mapper Mapper { get; private set; }

        public MapperConfiguration CreateMap()
        {
            this.Mapper = new Mapper();
            return this;
        }
    }
}
