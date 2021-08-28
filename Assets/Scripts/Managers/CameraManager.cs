using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; } = null;

    new public Camera camera;

    List<Command> commands = new List<Command>();
    private void Awake()
    {
        Instance = this;
    }

    public void AddCameraMove(Vector3 position, float speed)
    {
        Command command = new CamMove(position, speed);

        for (int i = 0; i < commands.Count; i++)
        {
            if(commands[i].GetType() ==  command.GetType())
            {
                commands[i].Release();
                commands.Remove(commands[i]);
            }
        }

        commands.Add(command);
    }
    public void AddCameraShake(float time, float power)
    {
        Command command = new CamShake(time, power);

        for (int i = 0; i < commands.Count; i++)
        {
            if (commands[i].GetType() == command.GetType())
            {
                commands[i].Release();
                commands.Remove(commands[i]);
            }
        }

        commands.Add(command);
    }
    public void AddCameraZoom(float camSize, float speed, float delay)
    {
        Command command = new CamZoom(camSize, speed, delay);

        for (int i = 0; i < commands.Count; i++)
        {
            if (commands[i].GetType() == command.GetType())
            {
                commands[i].Release();
                commands.Remove(commands[i]);
            }
        }

        commands.Add(command);
    }
    public void PlayAll()
    {
        foreach (Command command in commands) command.Excute();
    }
}

public interface Command
{
    public void Excute();
    public void Release();
}

public abstract class CameraActor : Command // Brigde 패턴을 위한 추상클래스
{
    protected Camera Camera => CameraManager.Instance.camera;
    protected Coroutine Coroutine { get; set; }
    public abstract void Excute();
    public abstract void Release();
}

public class CamMove : CameraActor
{
    Vector3 targetPos;
    float moveSpeed;
    public CamMove(Vector3 position, float speed)
    {
        Settings(position, speed);
    }

    void Settings(Vector3 position, float speed)
    {
        position.z = Camera.transform.position.z;

        targetPos = position;
        moveSpeed = speed;
    }

    public override void Excute()
    {
        Coroutine = CameraManager.Instance.StartCoroutine(_CamMove());
    }
    public override void Release()
    {
        if (Coroutine != null) CameraManager.Instance.StopCoroutine(Coroutine);
    }
    IEnumerator _CamMove()
    {
        while (true)
        {
            if (Vector2.Distance(Camera.transform.position, targetPos) <= 0.001f)
            {
                Camera.transform.position = targetPos;
            }
            yield return new WaitForSeconds(0.01f * Time.timeScale);

            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, targetPos, Time.deltaTime * moveSpeed);
        }
    }
}

public class CamShake : CameraActor
{
    float shakeTime;
    float shakePower;
    public CamShake(float time, float power)
    {
        Settings(time, power);
    }

    public void Settings(float time, float power)
    {
        shakeTime = time;
        shakePower = power;
    }

    public override void Excute()
    {
        Coroutine = CameraManager.Instance.StartCoroutine(_CamShake());
    }
    public override void Release()
    {
        if (Coroutine != null) CameraManager.Instance.StopCoroutine(Coroutine);
    }

    IEnumerator _CamShake()
    {
        float random_x, random_y;
        float startTime = Time.time;
        while (Time.time - startTime <= shakeTime)
        {
            random_x = Random.Range(-0.01f * shakePower, 0.01f * shakePower);
            random_y = Random.Range(-0.01f * shakePower, 0.01f * shakePower);

            Camera.transform.Translate(new Vector2(random_x, random_y));
            yield return new WaitForSeconds(0.0001f * Time.timeScale);
            Camera.transform.Translate(new Vector2(-random_x, -random_y));
        }
    }
}
public class CamZoom : CameraActor
{
    float startCamSize;
    float targetCamSize;
    float zoomSpeed;
    float zoomDelay;
    public CamZoom(float camSize, float speed, float delay)
    {
        Settings(camSize, speed, delay);
    }

    public void Settings(float camSize, float speed, float delay)
    {
        startCamSize = targetCamSize = camSize;
        zoomSpeed = speed;
        zoomDelay = delay;
    }

    public override void Excute()
    {
        Coroutine = CameraManager.Instance.StartCoroutine(_CamZoom());
    }
    public override void Release()
    {
        if (Coroutine != null) CameraManager.Instance.StopCoroutine(Coroutine);
    }
    IEnumerator _CamZoom()
    {
        while (true)
        {
            if (Mathf.Abs(Camera.orthographicSize - targetCamSize) <= 0.01f)
            {
                Camera.orthographicSize = targetCamSize;
                break;
            }
            yield return new WaitForSeconds(0.01f * Time.timeScale);

            Camera.orthographicSize = Mathf.MoveTowards(Camera.orthographicSize, targetCamSize, Time.deltaTime * zoomSpeed);
        }

        yield return new WaitForSeconds(zoomDelay);

        Camera.orthographicSize = startCamSize;
    }
}