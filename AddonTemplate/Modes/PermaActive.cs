using System;
using EloBuddy;
using EloBuddy.SDK;
//Actually i wont see long calls like config.bla.bla.bla so we will use
using Settings = AddonTemplate.Config.Modes;

namespace AddonTemplate.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Since this is permaactive mode, always execute the loop
            return true;
        }

        public override void Execute()
        {
            // First come the OnPostAttack then OnPreAttack so according to OOP rules will be
            //Orbwalker.OnPostAttack += Orbwalker_Post;
            Orbwalker.OnPreAttack += Orbwalker_Pre;
            Orbwalker.OnPostAttack += Orbwalker_Post;
        }

        /// <summary>
        ///     This function will make the orbwalker do a specific command first then cast the AA
        /// </summary>
        /// <param name="target">The attackable unit the target selector takes in consideration</param>
        /// <param name="args"></param>
        private void Orbwalker_Pre(AttackableUnit target, Orbwalker.PreAttackArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear) )
            {
                ModeManager.Useitems();
                Orbwalker.ResetAutoAttack();
            }
        }

        /// <summary>
        ///     This function will make the orbwalker do a specific command after the AA is casted
        /// </summary>
        /// <param name="target">The attackable unit the target selector takes in consideration</param>
        /// <param name="args"></param>
        private void Orbwalker_Post(AttackableUnit target, EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear) )
            {
                if (Settings.Combo.UseQ && SpellManager.Q.IsReady())
                {
                    //Every action that is not a boolean or floating var should go in the expression body
                    SpellManager.Q.Cast();
                    Orbwalker.ResetAutoAttack();
                }
            }
        }
    }
}