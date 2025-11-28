using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.Interfaces
{
    internal interface IMemberService
    {
        void ListMembers();
        void CreateMember();
        void UpdateMember();
        void DeleteMember();
        void SearchMemberByEmail();
    }
}
