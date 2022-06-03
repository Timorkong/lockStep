using AINodeCanvas.Framework;
using AIParadoxNotion.Design;


namespace AINodeCanvas.Tasks.Actions
{

    [Category("Camera")]
    public class FadeIn : ActionTask
    {

        public float fadeTime = 1f;

        protected override void OnExecute() {
            CameraFader.current.FadeIn(fadeTime);
        }

        protected override void OnUpdate() {
            if ( elapsedTime >= fadeTime )
                EndAction();
        }
    }
}