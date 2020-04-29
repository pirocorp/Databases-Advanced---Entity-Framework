namespace PetStore.Services
{
    public interface IBreedService
    {
        int Add(string name);

        bool Exists(int breedId);
    }
}