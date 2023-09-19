import { F_OptionalAggregateNav } from './f-optional-aggregate-nav.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToOne } from 'typeorm';

@Entity('f_optional_dependent')
export class F_OptionalDependent {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  optionalDepAttr: string;

  @OneToOne(() => F_OptionalAggregateNav, f_OptionalAggregateNav => f_OptionalAggregateNav.f_OptionalDependent, { cascade: ['insert', 'update'] })
  f_OptionalAggregateNav?: F_OptionalAggregateNav;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}