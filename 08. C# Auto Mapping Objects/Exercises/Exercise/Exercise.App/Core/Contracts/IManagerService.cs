namespace Exercise.App.Core.Contracts
{
    using Dtos;

    public interface IManagerService
    {
        SetManagerDto SetManager(int employeeId, int managerId);

        ManagerDto GetManagerInfo(int employeeId);
    }
}