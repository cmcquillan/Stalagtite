using NUnit.Framework;
using Stalagtite;
using Stalagtite.Actions;
using Stalagtite.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StalagtiteTests
{
    [TestFixture]
    public class ActionTests
    {
        [Test]
        public void ObjectsHaveAccessToAllAction()
        {
            var mob = new MobileObject(new GameState());
            Assert.IsTrue(mob.CanExecute(ActionType.AllAction));
            var player = new Player(new GameState(), null);
            Assert.IsTrue(player.CanExecute(ActionType.AllAction));
        }

        [Test]
        public void ObjectsHaveAccessToMobileAction()
        {
            var mob = new MobileObject(new GameState());
            Assert.IsTrue(mob.CanExecute(ActionType.MobileAction));
            var player = new Player(new GameState(), null);
            Assert.IsTrue(player.CanExecute(ActionType.MobileAction));
        }

        [Test]
        public void ObjectsHaveAccessToPlayerAction()
        {
            var player = new Player(new GameState(), null);
            Assert.IsTrue(player.CanExecute(ActionType.PlayerAction));
        }

        [Test]
        public void ObjectActionQueueAcceptsAppropriateActions()
        {
            var gs = new GameState();
            var p = new Player(gs, null);

            p.ActionQueue.Enqueue(new BroadcastAction(null));

            Assert.IsTrue(p.ActionQueue.HasNext());
        }

        [Test]
        public void ObjectActionQueueIgnoresInappropriateActions()
        {
            var gs = new GameState();
            var mob = new MobileObject(gs);

            mob.ActionQueue.Enqueue(new ExitAction(null));
            Assert.IsFalse(mob.ActionQueue.HasNext());
        }
    }
}
