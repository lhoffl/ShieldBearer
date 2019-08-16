using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    
    public int selected_index = 0;

    public Text menu;

    public Image logo;
    public Sprite for_the_real_fans;

    /* 
    public Color default_color;
    public Color selected_color;
    */
    private List<string> button_list;

    private string[] left_select = {"||", "|>|", "|>>|", "|>>>|", "|>>>>|", "|>>>>>|"};
    private string[] right_select = {"||", "|<|", "|<<|", "|<<<|", "|<<<<|", "|<<<<<|"};

    public int hold_interval = 0;

    public float hold_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        button_list = new List<string>();

        button_list.Add("Play");
        button_list.Add("Credits");
        button_list.Add("Exit");
        

        //foreach(Text t in GetComponentsInChildren<Text>()) buttonList.Add(t);
    }

    // Update is called once per frame
    void Update()
    {

        if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreController>().GetLastScore() >= 1000) {
            logo.sprite = for_the_real_fans;
            logo.GetComponent<RectTransform>().localScale = new Vector3(0.5f,0.5f,0f);
        } 
        /*
        int index = 0;
        foreach(Text t in buttonList)
        {
            if(index == selected_index) t.color = selected_color;
            else t.color = default_color;

            index++;
        }
        */
        
        

        if ((Input.GetKeyUp("space")))
        {
            Debug.Log(hold_interval.ToString());
            if (hold_interval >= 3)
            {
                Debug.Log("Selected!");
                CheckSelection();
            }
            else SelectNextItem();
        }
         
        if ((Input.GetKey("space"))) IncreaseInterval();
        else
        {
            hold_interval = 0;
            hold_time = 0;
        }

        UpdateTextBox();
    }


    void UpdateTextBox()
    {
        string menu_text = "";
        int index = 0;
        foreach(string s in button_list)
        {
            if(index == selected_index) menu_text += "\n" + left_select[hold_interval] + s + right_select[hold_interval];
            else menu_text += "\n" + s;

            index++;
        }

        menu.text = menu_text;
    }

    void SelectNextItem()
    {
        selected_index++;
        if(selected_index > button_list.Count - 1) selected_index = 0;
    }

    void IncreaseInterval()
    {
        //if (hold_interval == 0) hold_time = 0;

        hold_time += Time.deltaTime;

        hold_interval = (int)(hold_time * 2);

        if (hold_interval > 3) hold_interval = 3;
    }

    void CheckSelection()
    {
        Debug.Log("CheckSelection fired.");
        switch(selected_index)
        {
            case 0:
                //Play Game
                Debug.Log("Play Selected");
                if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreController>().GetLastScore() >= 666) {
                    GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().StartBonusLoop();
                } else {
                    GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().StartMainBGMLoop();
                }
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>().enabled = true;
                SceneManager.LoadScene("GameScene");
                break;
            case 1:
                //Credits
                Debug.Log("Credits Selected");
                SceneManager.LoadScene("Credits");
                break;
            case 2:
                //Exit
                Application.Quit();
                Debug.Log("Quit Selected");
                break;
        }
    }
}
