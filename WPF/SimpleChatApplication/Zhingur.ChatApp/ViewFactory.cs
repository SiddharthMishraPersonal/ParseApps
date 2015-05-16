namespace Zhingur.ChatApp
{
    using Zhingur.UserControls.UserControls;
    using System.ComponentModel.Composition;

    [Export]
    public class ViewFactory
    {
        #region Private Member Variables


        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        [ImportingConstructor]
        public ViewFactory(ucChatHistoryView ucChatHistory)
        {

        }

        #endregion

        #region Public Methods


        #endregion

        #region Private Methods


        #endregion
    }
}
