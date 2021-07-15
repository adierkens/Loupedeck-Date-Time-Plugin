namespace Loupedeck.DateTimePlugin
{
    using System;

    public class ClockApp : ClientApplication
    {

    }

    public class ClockPlugin : Plugin
    {
        public override Boolean HasNoApplication => true;

        public override void ApplyAdjustment(String adjustmentName, String parameter, Int32 diff)
        {

        }

        public override void RunCommand(String commandName, String parameter)
        {

        }
    }

    public class ClockCommand : PluginDynamicCommand
    {
        private readonly System.Timers.Timer timer;

        public ClockCommand() : base("Custom Date/Time", "Clock", "")
        {
            this.timer = new System.Timers.Timer(1000);
            this.timer.Elapsed += (sender, e) =>
            {
                this.ActionImageChanged("format");
            };
            this.timer.AutoReset = true;
            this.timer.Start();


            this.MakeProfileAction("text; Enter a C# date format. (h:mm) See: https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings");
            this.AddParameter("format", "Format", "");
        }

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            if (actionParameter != null)
            {
                return DateTime.Now.ToString(actionParameter);
            }

            return "";
        }

        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmap = new BitmapBuilder(imageSize);
            bitmap.FillRectangle(0, 0, 90, 90, BitmapColor.Black);

            bitmap.DrawText(this.GetCommandDisplayName(actionParameter, imageSize), BitmapColor.White, 20);
            return bitmap.ToImage();
        }
    }
}
