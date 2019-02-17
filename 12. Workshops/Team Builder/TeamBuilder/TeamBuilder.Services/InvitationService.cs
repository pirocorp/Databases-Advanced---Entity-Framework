namespace TeamBuilder.Services
{
    using System.Linq;
    using AutoMapper;

    using Data;
    using Interfaces;
    using Models;

    public class InvitationService : GenericService<Invitation>, IInvitationService
    {
        public InvitationService(TeamBuilderContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public Invitation Invite(int invitedUserId, int teamId)
        {
            var invitation = new Invitation()
            {
                InvitedUserId = invitedUserId,
                TeamId = teamId
            };

            this.Add(invitation);
            return invitation;
        }

        public void Accept(int invitedUserId, int teamId)
        {
            this.DeactivateInvitation(invitedUserId, teamId);
        }

        public void Decline(int invitedUserId, int teamId)
        {
            this.DeactivateInvitation(invitedUserId, teamId);
        }

        private void DeactivateInvitation(int invitedUserId, int teamId)
        {
            var invite = this.GetAll(i => i.InvitedUserId == invitedUserId &&
                                          i.TeamId == teamId &&
                                          i.IsActive)
                .First();

            invite.IsActive = false;
            this.Update(invite);
        }
    }
}