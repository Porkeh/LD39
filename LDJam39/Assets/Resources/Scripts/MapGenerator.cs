using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    Vector3 position;
    enum Contains { blank, wall,player  };
    Contains contains;

    public Node()
    {

    }

    public Node(Vector3 position, int i)
    {
        this.position = position;
        switch (i)
        {
            case 0:

                contains = Contains.blank;
                break;
            case 1:

                contains = Contains.wall;
                break;
            case 2:
                contains = Contains.player;
                break;



        }



    }

    public Vector3 GetPosition()
    {
        return this.position;
    }

    public int GetContains()
    {
        return (int)contains;
    }



}


public class MapGenerator : MonoBehaviour {


    public GameObject rock;
    public GameObject player;
    public GameObject tentacle;
    public GameObject shark;
    public GameObject bomb;
    private List<Node> nodes;
    public int xSize;
    public int ySize;
    float nodeheight;
    float nodewidth;

    Node PeekAbove(Node n)
    {

        int currentIndex = nodes.IndexOf(n);


        int newIndex = currentIndex - ySize;
        if (newIndex > 0)
        {
            Node nodeAbove = nodes[newIndex];
            return nodeAbove;
        }
        return null;


    }
    void GenerateMap()
    {
        int contains =0;
        for(int j = 0; j < xSize; j++)
        {
            for(int i = 0; i < ySize; i++)
            {
                contains = 0;

                int rand = Random.Range(0, 100);
                

          

                if(rand >= 70)
                {
                    contains = 1;
                    rock.tag = "Untagged";
                    rock.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 1);
                    if(rand >= 95)
                    {
                        rock.tag = "Indestructable";
                        rock.GetComponent<SpriteRenderer>().color = new Vector4(0.4f, 0.4f, 0.4f, 1);
                    }
                }

                if (i * nodeheight > player.transform.position.x - 2 && i * nodeheight < player.transform.position.x + 2)
                {
                    if (j * nodeheight > player.transform.position.y - 2 && j * nodeheight < player.transform.position.y + 2)
                    {
                        contains = 2;
                    }

                }
                if( i == 0 ||j == 0 || i == xSize-1 )
                {
                    contains = 1;
                    rock.tag = "Indestructable";
                    rock.GetComponent<SpriteRenderer>().color = new Vector4(0.4f, 0.4f, 0.4f, 1);
                }

                
                



                Node n = new Node(new Vector3(nodewidth * i,nodeheight * j ,0), contains);

                nodes.Add(n);

                switch (contains)
                {
                    case 0:
                        rand = Random.Range(0, 100);
                        if(rand < 5)
                        {
                            Instantiate(shark, n.GetPosition(), Quaternion.identity);
                        }
                        else
                        {
                            rand = Random.Range(0, 100);
                            if(rand < 5)
                            {
                                
                                    Instantiate(bomb, n.GetPosition(), Quaternion.identity);
                                
                            }
                        }
                        break;
                    case 1:

                        Instantiate(rock, n.GetPosition(), Quaternion.identity);

                        rand = Random.Range(0, 100);

                        if(rand > 80)
                        {

                            Node temp = PeekAbove(n);
                            if (temp != null)
                            {
                                if (temp.GetContains() == 0)
                                {

                                    Instantiate(tentacle, temp.GetPosition(), Quaternion.Euler(0, 0, 180)); 
                                }
                            }
                        }
                        

                        break;


                }
                

                


            }



        }

    }


	// Use this for initialization
	void Start () {
        
        nodes = new List<Node>();
        Random.InitState(System.DateTime.Now.Millisecond);

        nodeheight = 0.318f *2;
        nodewidth = 0.318f *2;

        GenerateMap();


        
		

	}
	
	// Update is called once per frame
	void Update () {

        int i = 0;
		
	}
}
