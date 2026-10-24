namespace Core.Object.Module
{
    public static class SearchedSlotResultExtensions
    {
        public static bool IsAttachable(this SearchedSlotResult result)
        {
            return result.Slot != null &&
                   result.Slot.State == SlotState.Attachable &&
                   result.Slot.Socket == null;
        }
    }
}
