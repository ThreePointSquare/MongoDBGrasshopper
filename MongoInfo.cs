using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace MonogDB
{
    public class MongoDBInfo : GH_AssemblyInfo
    {
        public override string Name => "MongoDB";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "Manipulate MongoDB databses with Grasshopper and Rhino data";

        public override Guid Id => new Guid("9416efa4-74cb-4536-a56d-259306348860");

        //Return a string identifying you or your company.
        public override string AuthorName => "Craig Forneris";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "threepointsquare@gmail.com";
    }
}