namespace Game.UI.Minigames.Manhole
{
    using System;

    [Serializable]
    public class QuestionData
    {
        public string question;
        public string[] options; // Four options
        public int correctIndex; // Index 0-3
    }
}