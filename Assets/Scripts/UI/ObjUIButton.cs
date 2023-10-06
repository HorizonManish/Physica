using UnityEngine;
using UnityEngine.UI;

public class ObjUIButton : MonoBehaviour
{
    //Panel prefabs
    [SerializeField] private GameObject rigidbodyPanelPrefab;
    [SerializeField] private GameObject transformPanelPrefab;
    [SerializeField] private GameObject materialPanelPrefab;
    //Obj components used in panels
    Material materialOfPanel;
    Rigidbody rigidbodyOfPanel;
    Transform transformOfPanel;

    GameObject Panel;
    bool panelAlreadyInstantiated;
    bool panelIsVisible;
    public Text buttonText;
    enum PanelTypeSelector
    {
        Transform,
        Rigidbody,
        Material
    }
    PanelTypeSelector panelType;
 
    public void RigidbodyPanel(Rigidbody rigidbody)
    {

    }
    public void TransformPanel(Transform transform)
    {

    }
    public void MaterialPanel(Material material)
    {
        buttonText.text = "Material";
        panelType = PanelTypeSelector.Material;
        materialOfPanel = material;

        //only the position of material panel is not right while instantiating
        //Panel.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().p;
        
       
    }
    
    public void OnClick()
    {
        if(!panelAlreadyInstantiated)
        {
            switch (panelType)
            {
                case PanelTypeSelector.Material:
                    Panel = Instantiate(materialPanelPrefab);                                    
                    Panel.GetComponent<MaterialPanel>().GetProperties(materialOfPanel);                  
                    break;
                case PanelTypeSelector.Rigidbody:
                    break;
                case PanelTypeSelector.Transform:
                    break;
            }
            //   Panel.transform.position = transform.position;
            Panel.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            panelAlreadyInstantiated = true;
            Panel.transform.SetParent(GameobjectPanel.Instance.transform);
        }
        else if(panelAlreadyInstantiated)
        {           
            if(panelIsVisible)
            {
                PanelSetActive(false);
            }
             else
            {
                PanelSetActive(true);
            }
        }
        void PanelSetActive(bool state)
        {            
            if(Panel!=null)
            {
                panelIsVisible = state;
                Panel.SetActive(state);
            }
        }
        
    }
}
