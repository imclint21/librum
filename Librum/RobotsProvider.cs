using System.Collections.Generic;
using Robotify.AspNetCore;

namespace Librum
{
    public class RobotsProvider : IRobotifyRobotGroupProvider
    {
        public IEnumerable<RobotGroup> Get()
        {
            yield return new RobotGroup()
            {
                UserAgent = "*",
                Disallow = new[] { "/account" }
            };
        }
    }
}