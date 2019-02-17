namespace TeamBuilder.Services.Interfaces
{
    using Models;

    public interface IInvitationService : IGenericService<Invitation>
    {
        Invitation Invite(int invitedUserId, int teamId);

        void Accept(int invitedUserId, int teamId);

        void Decline(int invitedUserId, int teamId);
    }
}