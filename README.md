# A* Pathfinding Library

Этот пакет предоставляет систему для поиска пути с использованием алгоритма A* в Unity. Он состоит из нескольких компонентов, включая управление сеткой, обработку узлов и сам алгоритм поиска пути. Следуйте этим шагам, чтобы настроить и использовать его.

## Требования

1. Для работы требуется [High speed priority queue](https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp)

## Шаг 1: Добавление скрипта GridManager

1. Создайте новый пустой объект в Unity.
2. Назовите его, например, `GridManager`.
3. Добавьте к объекту скрипт `GridManager`. Это будет объект, который управляет вашей сеткой для поиска пути.

```csharp
using sudentaivals.CustomAstar;

public class Example : MonoBehaviour
{
    private void Start()
    {
        GridManager.Instance.SetGrid(yourPathfindingGrid);
    }
}
```

## Шаг 2: Настройка объекта с PathfindingGrid

1. Создайте новый объект в сцене для сетки.
2. Добавьте к объекту скрипт `PathfindingGrid`.
3. В инспекторе Unity настройте параметры сетки, такие как количество строк и столбцов, размер ячеек и другие параметры, необходимые для вашей сетки.
4. Вы можете добавить слой препятствий, чтобы отметить объекты, которые должны быть недоступны для пути.

```csharp
using sudentaivals.CustomAstar;

public class Example : MonoBehaviour
{
    public PathfindingGrid pathfindingGrid;

    private void Start()
    {
        pathfindingGrid.ActivateGrid();
    }
}
```

## Шаг 3: Создание объекта A* и настройка через GridManager

1. Создайте объект в сцене, который будет использовать алгоритм A*.
2. Добавьте к объекту скрипт `Astar`.
3. Убедитесь, что вы активировали и настроили объект с `PathfindingGrid` через `GridManager.Instance.SetGrid()`.
4. После этого вы можете использовать метод `FindPath()` для поиска пути между двумя точками.

```csharp
using sudentaivals.CustomAstar;
using UnityEngine;

public class Example : MonoBehaviour
{
    public Astar astar;
    public PathfindingGrid pathfindingGrid;

    private void Start()
    {
        // Убедитесь, что сетка активирована
        pathfindingGrid.ActivateGrid();
        GridManager.Instance.SetGrid(pathfindingGrid);

        // Пример поиска пути
        Vector3 startPos = new Vector3(0, 0, 0); // Начальная позиция
        Vector3 goalPos = new Vector3(10, 10, 0); // Конечная позиция

        Stack<Node> path = astar.FindPath(startPos, goalPos);

        // Действия с найденным путем
        if (path != null)
        {
            Debug.Log("Путь найден!");
            foreach (Node node in path)
            {
                Debug.Log(node.WorldPosition);
            }
        }
        else
        {
            Debug.Log("Путь не найден.");
        }
    }
}
```

## Шаг 4: Использование метода `FindPath`

Метод `FindPath` используется для поиска пути от одной точки к другой в сетке. Вы можете вызвать его, передав начальную и конечную позиции в виде объектов `Vector3`. Метод вернет стек узлов, представляющих путь.

Пример вызова метода для поиска пути:

```csharp
Stack<Node> path = astar.FindPath(startPos, goalPos);
```

Если путь найден, вы получите стек узлов. Если путь не найден, метод вернет `null`.
