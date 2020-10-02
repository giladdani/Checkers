using System;
using System.Windows.Controls;

namespace CheckersWinForms
{
    public class Move
    {
        // Private Members
        private int m_XFrom;
        private int m_YFrom;
        private int m_XTo;
        private int m_YTo;
        private bool m_IsQuit;

        // Constructors
        public Move()
        {
        }

        public Move(int i_YFrom, int i_XFrom, int i_YTo, int i_XTo)
        {
            m_YFrom = i_YFrom;
            m_XFrom = i_XFrom;
            m_YTo = i_YTo;
            m_XTo = i_XTo;
        }

        public Move(string i_Move)
        {
            if (i_Move[0] == 'Q')
            {
                m_IsQuit = true;
            }
            else
            {
                m_YFrom = i_Move[0] - 'A';
                m_XFrom = i_Move[1] - 'a';
                m_YTo = i_Move[3] - 'A';
                m_XTo = i_Move[4] - 'a';
            }
        }

        // Properties
        public int XFrom
        {
            get
            {
                return m_XFrom;
            }

            set
            {
                m_XFrom = value;
            }
        }

        public int YFrom
        {
            get
            {
                return m_YFrom;
            }

            set
            {
                m_YFrom = value;
            }
        }

        public int XTo
        {
            get
            {
                return m_XTo;
            }

            set
            {
                m_XTo = value;
            }
        }

        public int YTo
        {
            get
            {
                return m_YTo;
            }

            set
            {
                m_YTo = value;
            }
        }

        public bool IsQuit
        {
            get
            {
                return m_IsQuit;
            }

            set
            {
                m_IsQuit = value;
            }
        }
    }
}
