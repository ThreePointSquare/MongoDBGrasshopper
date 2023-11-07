using Grasshopper;
using Grasshopper.Kernel;
using MongoDB;
using MongoDB.Driver;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace MonogDB
{
    public class MongoConnectComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public MongoConnectComponent()
          : base("MongoConnect", "DBConnect",
            "Connects to MongoDB using Conneciton String provided by MongoDB",
            "MongoDB", "Connection")
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
            pManager.AddBooleanParameter("Connect", "run", "Open Connection", GH_ParamAccess.item, false);
            pManager.AddTextParameter("Connect String", "string", "Connection string from MongoDB", GH_ParamAccess.item);
  

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
            pManager.AddGenericParameter("client", "client", "MongoDB Client", GH_ParamAccess.item);

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

            Boolean run = false;
            String connectstring = "mongodb + srv://<demo>:<password>@cluster.texttext.mongodb.net/?retryWrites=true&w=majority";


            // Then we need to access the input parameters individually. 
            // When data cannot be extracted from a parameter, we should abort this method.
            if (!DA.GetData(0, ref run)) return;
            if (!DA.GetData(1, ref connectstring)) return;

            if (run == true)
            {
                try
                {
                    var connection = CreateConnection(connectstring);



                    DA.SetData(1, connection);
                    DA.SetData(0, "Success!");
                }
                catch (Exception e)
                {
                    DA.SetData(0, e);
                }
                

  
            }
        }

        public static object CreateConnection(string connectstring)
        {

            var client = Helpers.SimpleConnect(connectstring);
           

            return client;
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
        protected override System.Drawing.Bitmap Icon => MongoDB.Properties.Resources.mongconnect;



        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("973cb59c-0154-444d-9c02-cd7104c9ff93");
    }
}