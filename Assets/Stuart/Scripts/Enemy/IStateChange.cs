namespace Stuart.Scripts.Enemy
{
	public interface IStateChange
	{
		public void ChangeState(BaseState newState);
	}
}