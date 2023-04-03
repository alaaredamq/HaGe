﻿namespace HaGe.Application.Helpers; 

public static class Helper {
    public static void Move<T>(this List<T> list, T item, int newIndex)
    {
        if (item != null)
        {
            var oldIndex = list.IndexOf(item);
            if (oldIndex > -1)
            {
                list.RemoveAt(oldIndex);

                if (newIndex > oldIndex) newIndex--;
                // the actual index could have shifted due to the removal

                list.Insert(newIndex, item);
            }
        }

    }
}