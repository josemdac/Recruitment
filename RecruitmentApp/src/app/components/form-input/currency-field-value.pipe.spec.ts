import { CurrencyFieldValuePipe } from './currency-field-value.pipe';

describe('CurrencyFieldValuePipe', () => {
  it('create an instance', () => {
    const pipe = new CurrencyFieldValuePipe();
    expect(pipe).toBeTruthy();
  });
});
