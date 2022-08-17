using UnityEngine;


public class GameManager : MonoBehaviour
{
    private GameObject _selectedObject;
    private GameObject _selectedStand;

    private Circle _circle;
    public bool isMoved;

    public int targetStandValue;
    private int _completedStandValue;

    [SerializeField] private AudioSource[] sounds;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (_selectedObject != null && _selectedStand != hit.collider.gameObject)
                    {
                        // Cember gonderme yeri (baska yerdeki standa gonderme islemi)
                        Stand _stand = hit.collider.GetComponent<Stand>();

                        if (_stand.circle.Count != 4 && _stand.circle.Count != 0)
                        {
                            if (_circle.color == _stand.circle[_stand.circle.Count - 1].GetComponent<Circle>().color)
                            {
                                SetSocket(_stand, hit);
                            }
                            else
                            {
                                SocketBackFunction();
                            }

                        }
                        else if (_stand.circle.Count == 0)
                        {
                            SetSocket(_stand, hit);
                        }
                        else
                        {
                            SocketBackFunction();
                        }

                    }
                    else if (_selectedStand == hit.collider.gameObject)
                    {
                        // Standa tekrar tiklandginda tekrar yerine getirmek icin koyuldu.
                        SocketBackFunction();
                    }
                    else
                    {
                        // Standa tiklandiginde ilk etapta calisan yerdir.
                        BackStand(hit);
                    }
                }
            }
        }
    }

    private void SetSocket(Stand _stand, RaycastHit hit)
    {
        sounds[2].Play();
        _selectedStand.GetComponent<Stand>().ChangeSocketControls(_selectedObject);

        _circle.Move("ChangePosition", hit.collider.gameObject, _stand.GetEmptySocket(), _stand.movePosition);

        _stand.emptySocket++;
        _stand.circle.Add(_selectedObject);
        _stand.CheckCircle();

        _selectedObject = null;
        _selectedStand = null;


    }
    private void SocketBackFunction()
    {
        sounds[1].Play();
        _circle.Move("SocketBack");
        _selectedObject = null;
        _selectedStand = null;
    }

    private void BackStand(RaycastHit hit)
    {
        Stand stand = hit.collider.GetComponent<Stand>();
        _selectedObject = stand.GetFirstCircle();
        _circle = _selectedObject.GetComponent<Circle>();
        isMoved = true;

        if (_circle.isMove)
        {
            _circle.Move("Selected", null, null, _circle.locatedStand.GetComponent<Stand>().movePosition);
            _selectedStand = _circle.locatedStand;
        }
    }
    public void CompletedStand()
    {
        _completedStandValue++;
        if (_completedStandValue == targetStandValue)
        {
            Debug.Log("WIN WINDOW");
        }
    }
    public void OnClickButtonControls(string butonValue)
    {
        switch (butonValue)
        {
            case "Restart":
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
                break;
            case "Menu":
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                break;
            case "Next":
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+1);
                break;
        }
    }
}
