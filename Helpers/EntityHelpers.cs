using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Notifications.Management;
using Colossal.PSI.Common;
using Colossal.UI.Binding;
using Game.Simulation;
using Game.UI.InGame;
using Unity.Entities;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
using BepInEx.Logging;
using Colossal.CharacterSystem.Camera;
using Game.Prefabs;
using UnityEngine;
using Game.Triggers;
using Unity.Collections;

namespace TargetMethodsDemo.Helpers
{
    public class EntityHelpers
    {
        public World GetWorld()
        {
            return World.All.FirstOrDefault(world => world is { Name: "Game" });
        }
        private async void SyncNotifications()
        {
            // Get the listener
            UserNotificationListener listener = UserNotificationListener.Current;

            // And request access to the user's notifications (must be called from UI thread)
            UserNotificationListenerAccessStatus notificationListenerAccessStatus = await listener.RequestAccessAsync();
            switch (notificationListenerAccessStatus)
            {
                // This means the user has granted access.
                case UserNotificationListenerAccessStatus.Allowed:
                    IReadOnlyList<UserNotification> notifs = await listener.GetNotificationsAsync(NotificationKinds.Toast);

                    foreach (var userNotification in notifs)
                    {
                        //Unity.Jobs.JobHandle deps;
                        var em = GetWorld().EntityManager;
                        var chirp = new ChirpPrefab
                        {
                            active = true,
                            name = "test"
                        };






                        //GetWorld().GetExistingSystemManaged<CreateChirpSystem>().GetQueue(out deps).Enqueue();
                        //actionBuffer.Enqueue(, Entity.Null, 0.0f));
                        //Game.Triggers.Chirp


                    }
                    // Get the toast notifications
                    //notifs = await listener.GetNotificationsAsync(NotificationKinds.Toast);
                    //Debug.WriteLine("Size of current notification buffer: " + notifs.Count());
                    //AcHelper.ProcessNotification(notifs.LastOrDefault());
                    break;

                // This means the user has denied access.
                // Any further calls to RequestAccessAsync will instantly
                // return Denied. The user must go to the Windows settings
                // and manually allow access.
                case UserNotificationListenerAccessStatus.Denied:
                    Debug.Log("UserNotificationListenerAccessStatus.Denied");
                    // Show UI explaining that listener features will not
                    // work until user allows access.
                    break;

                // This means the user closed the prompt without
                // selecting either allow or deny. Further calls to
                // RequestAccessAsync will show the dialog again.
                case UserNotificationListenerAccessStatus.Unspecified:
                    Debug.Log("UserNotificationListenerAccessStatus.Unspecified");
                    // Show UI that allows the user to bring up the prompt again
                    break;
            }
        }

      
        
        public void TestIt()
        {
            
            /**
             *
             *
             DebugUI.Button button41 = new DebugUI.Button();
      
            button41.displayName = "Create Chirps";
            button41.action = (System.Action) (() =>
            {
                // ISSUE: reference to a compiler-generated method
                NativeQueue<TriggerAction> actionBuffer = this.World.GetExistingSystemManaged<TriggerSystem>().CreateActionBuffer();
                actionBuffer.Enqueue(new TriggerAction(TriggerType.NoOutsideConnection, Entity.Null, 0.0f));
                actionBuffer.Enqueue(new TriggerAction(TriggerType.UnpaidLoan, Entity.Null, 999f));
            });
            */
            var world = GetWorld() ?? throw new Exception("No World");
            var chirp = world.GetOrCreateSystemManaged<ChirperUISystem>();
            chirp.BindChirp(new JsonWriter("Test"), Entity.Null, true);
        }
    }
}
