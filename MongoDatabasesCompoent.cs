using Grasshopper;
using Grasshopper.Kernel;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace MonogDB
{
    public class MongoListDatabasesComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public MongoListDatabasesComponent()
          : base("MongoListDB's", "DBList",
            "Lists databases in cluster from established Mongo Client",
            "MongoDB", "Database")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            // Use the pManager object to register your input parameters.
            // You can often supply default values when creating parameters.
            // All parameters must have the correct access type. If you want 
            // to import lists or trees of values, modify the ParamAccess flag.
            pManager.AddGenericParameter("MongoClient", "client", "Mongo Client", GH_ParamAccess.item);

  

            // If you want to change properties of certain parameters, 
            // you can use the pManager instance to access them by index:
            //pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)

        {
            // Use the pManager object to register your output parameters.
            // Output parameters do not have default values, but they too must have the correct access type.
            pManager.AddTextParameter("result", "result", "result", GH_ParamAccess.item);
            pManager.AddGenericParameter("databases", "databases", "DataBases", GH_ParamAccess.list);

            // Sometimes you want to hide a specific parameter from the Rhino preview.
            // You can use the HideParameter() method as a quick way:
            //pManager.HideParameter(0);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // First, we need to retrieve all data from the input parameters.
            // We'll start by declaring variables and assigning them starting values.


            MongoClient client = new MongoClient();


       
            if (!DA.GetData(0, ref client)) return;

            {
                try
                {
   
                    List<BsonDocument> databases = Helpers.ListDBs(client);

                    //TODO :: Normalize name and /or add capability to filter by key or value (BSON returns to json)


                    DA.SetDataList(1,databases);
                    DA.SetData(0, "Success!");
                }
                catch (Exception e)
                {
                    DA.SetData(0, e);
                }
            }
        }


        /// <summary>
        /// The Exposure property controls where in the panel a component icon 
        /// will appear. There are seven possible locations (primary to septenary), 
        /// each of which can be combined with the GH_Exposure.obscure flag, which 
        /// ensures the component will only be visible on panel dropdowns.
        /// </summary>
        public override GH_Exposure Exposure => GH_Exposure.primary;

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => MongoDB.Properties.Resources.mongdblistdbs;



        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("65c2cdbb-0aad-48c5-975b-833b5e236426");
    }
}