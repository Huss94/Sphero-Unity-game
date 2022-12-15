using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progessBar : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    private Player player;
    private GameObject Fill;
    private Image fill;
    private ParticleSystem ps;

    private Color yellow = new Color(0.725f,0.721f,0,1);
    private Color blue = new Color(0.117f, 0.129f, 0.596f, 1);

    [System.NonSerialized] public bool using_power;

    void Start()
    {
        slider = GetComponent<Slider>();
        player = GameObject.Find("Player").GetComponent<Player>();
        Fill = GameObject.Find("Fill");
        fill = Fill.GetComponent<Image>();
        ps = Fill.GetComponentInChildren<ParticleSystem>();
        init();
    }
    public void init(){
        slider.value = 0;
        using_power = false;
        fill.color = yellow;
        player.can_use_power = false;
    }

    // Update is called once per frame
    void Update()

    {
        test_value();
    }


    public void test_value(){
        if (slider.value >= slider.maxValue){
            player.can_use_power = true;
            fill.color = blue;

        }
    }

    public IEnumerator use_power(float last_time){
        // last_time represent the time the power last
        using_power = true;
        int n_iter = 100;
        player.speed_multiplier = 1.5f;
        ps.Play();
        int n = 0;

        for (float i = slider.maxValue; i > slider.minValue; i-= slider.maxValue/n_iter){
            slider.value = i;
            if (n > 80 ){
                player.speed_multiplier = 1;
            }

            n += 1;
            yield return new WaitForSeconds(last_time/n_iter);
        }
        slider.value = slider.minValue;
        fill.color= yellow;
        using_power = false;
        ps.Stop();
    }

    public void addValue(float v = 1, bool force = false){
        if(!using_power | force){
            slider.value += v;
        }
    }


    public bool is_using_power(){
        return using_power;
    }
}
