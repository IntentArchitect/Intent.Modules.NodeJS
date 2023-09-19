import { TptFkBaseClass } from './tpt-fk-base-class.entity';
import { Entity, Column } from 'typeorm';

@Entity('tpt_fk_derived_class')
export class TptFkDerivedClass extends TptFkBaseClass {
  @Column()
  derivedField: string;

}