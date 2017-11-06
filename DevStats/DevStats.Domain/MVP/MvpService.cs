﻿using System;
using System.Collections.Generic;
using System.Linq;
using DevStats.Domain.Security;

namespace DevStats.Domain.MVP
{
    public class MvpService : IMvpService
    {
        private readonly IUserRepository userRepository;
        private readonly IMvpRepository mvpRepository;

        public MvpService(IMvpRepository mvpRepository, IUserRepository userRepository)
        {
            if (mvpRepository == null) throw new ArgumentNullException(nameof(mvpRepository));
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));

            this.mvpRepository = mvpRepository;
            this.userRepository = userRepository;
        }

        public Dictionary<int, string> GetVotableUsers(int currentUserId)
        {
            var validUserRoles = new string[] { "User", "Admin" };

            return userRepository.Get()
                                 .Where(x => validUserRoles.Contains(x.Role) && x.Id != currentUserId)
                                 .ToDictionary(x => x.Id, y => y.UserName);
        }

        public VoteResult Vote(int voteeId, int voterId, string description)
        {
            if (voteeId == voterId) return new VoteResult(false, "You cannot vote for yourself.");
            if (string.IsNullOrWhiteSpace(description)) return new VoteResult(false, "You cannot vote without giving a reason");

            try
            {
                var users = userRepository.Get();

                if (!users.Any(x => x.Id == voteeId)) return new VoteResult(false, "Unrecognised Votee.");
                if (!users.Any(x => x.Id == voterId)) return new VoteResult(false, "Unrecognised Voter.");

                mvpRepository.Vote(voteeId, voterId, description);

                return new VoteResult(true, "Thank you for voting.");
            }
            catch (Exception)
            {
                return new VoteResult(false, "Unknown Error.");
            }
        }

    }
}