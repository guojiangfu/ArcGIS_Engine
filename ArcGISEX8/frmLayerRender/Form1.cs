using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmLayerRender
{
    public partial class frmLayerRender : Form
    {
        public frmLayerRender()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //frmSymbolSelector symbolForm = new frmSymbolSelector();
            ////  ISimpleRenderer simpleRenderer = (ISimpleRenderer)m_featLayer.Renderer;
            ////Get the IStyleGalleryItem  
            //IStyleGalleryItem styleGalleryItem = null;
            ////Select SymbologyStyleClass based upon feature type  
            //styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassMarkerSymbols, m_sRen.Symbol);
            ////Create a new renderer  
            //if (styleGalleryItem == null)
            //{
            //    return;
            //}
            //m_sRen = new SimpleRendererClass();
            ////Set its symbol from the styleGalleryItem 
            //ISymbol pSym = (ISymbol)styleGalleryItem.Item;
            //IMarkerSymbol pMarkSym = (IMarkerSymbol)pSym; 

            //m_sRen.Symbol = pSym;

            //Bitmap b = Sym2Bitmap(pSym, (int)pMarkSym.Size, (int)pMarkSym.Size);
            //btnBmp.Image = (Image)b; 
            ////Set the renderer into the geoFeatureLayer 

            ////     m_featRender = (IFeatureRenderer)simpleRenderer; 
        }
    }
  }
